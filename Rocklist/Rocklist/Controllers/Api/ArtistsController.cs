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
			var result = new {
				_links = new {
					self = new { href = $"/api/artists/{slug}" },
					albums = new { href = $"/api/artists/{slug}/albums" }
				},
				name = artist.Name
			};
			return Ok(result);
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

		// PUT: api/Artists/5
			// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
			[HttpPut("{id}")]
		public async Task<IActionResult> PutArtist(Guid id, Artist artist) {
			if (id != artist.Id) {
				return BadRequest();
			}

			db.Entry(artist).State = EntityState.Modified;

			try {
				await db.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ArtistExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Artists
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Artist>> PostArtist(Artist artist) {
			db.Artists.Add(artist);
			await db.SaveChangesAsync();

			return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
		}

		// DELETE: api/Artists/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteArtist(Guid id) {
			var artist = await db.Artists.FindAsync(id);
			if (artist == null) {
				return NotFound();
			}

			db.Artists.Remove(artist);
			await db.SaveChangesAsync();

			return NoContent();
		}

		private bool ArtistExists(Guid id) {
			return db.Artists.Any(e => e.Id == id);
		}
	}
}
