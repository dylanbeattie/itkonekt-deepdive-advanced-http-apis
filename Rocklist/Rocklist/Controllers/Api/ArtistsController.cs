using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;
using Rocklist.Data.Entities;

namespace Rocklist.Controllers.Api {
	[Route("api/[controller]")]
	[ApiController]
	public class ArtistsController(RocklistDbContext db) : ControllerBase {

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
			=> await db.Artists.ToListAsync();

		[HttpGet("{slug}")]
		public async Task<IActionResult> GetArtist(string slug) {
			var artist = await db.Artists
				.Include(a => a.Albums)
				.ThenInclude(album => album.Tracks)
				.FirstOrDefaultAsync(
				a => a.Slug == slug
			);
			if (artist == null) return NotFound();
			return Ok(artist.ToResource());
		}

		[HttpGet("{slug}/albums")]
		public async Task<IActionResult> GetAlbumsByArtist(string slug) {
			var artist = await db.Artists
				.Include(a => a.Albums)
				.FirstOrDefaultAsync(a => a.Slug == slug);
			if (artist == null) return NotFound();
			var result = artist.Albums.Select(album => new {
				_links = new {
					self = new {
						href = $"/artists/{slug}/albums/{album.Slug}"
					},
					artist = new {
						href = $"/artists/{slug}"
					},
					tracks = new {
						href = $"/artists/{slug}/albums/{album.Slug}/tracks"
					}
				},
				name = album.Name
			}).ToArray();
			return Ok(result);
		}

		[HttpPut("{slug}")]
		public async Task<IActionResult> PutArtist(string slug, Artist artist) {
			var existingArtist = await db.Artists.FirstOrDefaultAsync(a => a.Slug == slug);
			if (existingArtist != null && artist.Id != default && existingArtist.Id != artist.Id)
				return Conflict("ID already assigned to a different record");
			IActionResult result;
			if (existingArtist != null) {
				db.Entry(existingArtist).CurrentValues.SetValues(artist);
				result = Ok(artist.ToResource());
			} else {
				db.Artists.Add(artist);
				result = Created($"/api/artists/{artist.Slug}", artist.ToResource());
			}
			await db.SaveChangesAsync();
			return result;

		}

		[HttpPost]
		public async Task<ActionResult<Artist>> PostArtist(Artist artist) {
			db.Artists.Add(artist);
			await db.SaveChangesAsync();
			return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
		}

		[HttpDelete("{slug}")]
		public async Task<IActionResult> DeleteArtist(string slug) {
			var artist = await db.FindArtistBySlug(slug);
			if (artist == null) return NotFound();
			db.Artists.Remove(artist);
			await db.SaveChangesAsync();
			return NoContent();
		}
	}
}
