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

		// GET: api/Artists/5
		[HttpGet("{slug}")]
		public async Task<ActionResult<Artist>> GetArtist(string slug) {
			var artist = await db.Artists
				.Include(a => a.Albums)
				.ThenInclude(album => album.Tracks)
				.FirstOrDefaultAsync(
				a => a.Slug == slug
			);
			if (artist == null) return NotFound();
			return artist;
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
