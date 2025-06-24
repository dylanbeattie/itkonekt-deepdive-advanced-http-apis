using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;
using Rocklist.Data.Entities;

namespace Rocklist.Controllers.Api {
	[ApiController]
	[Route("api/[controller]")]
	public class TracksController(RocklistDbContext db) : ControllerBase {

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
			=> await db.Tracks.ToListAsync();


		// PUT: api/Tracks/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTrack(Guid id, Track track) {
			if (id != track.Id) {
				return BadRequest();
			}

			db.Entry(track).State = EntityState.Modified;

			try {
				await db.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!TrackExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Tracks
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Track>> PostTrack(Track track) {
			db.Tracks.Add(track);
			await db.SaveChangesAsync();

			return CreatedAtAction("GetTrack", new { id = track.Id }, track);
		}

		// DELETE: api/Tracks/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTrack(Guid id) {
			var track = await db.Tracks.FindAsync(id);
			if (track == null) {
				return NotFound();
			}

			db.Tracks.Remove(track);
			await db.SaveChangesAsync();

			return NoContent();
		}

		private bool TrackExists(Guid id) {
			return db.Tracks.Any(e => e.Id == id);
		}
	}
}
