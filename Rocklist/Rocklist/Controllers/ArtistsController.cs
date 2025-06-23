using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;
using Rocklist.Data.Entities;

namespace Rocklist.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController(RocklistDbContext db) : ControllerBase {
        [HttpGet]
        public ActionResult<IEnumerable<Artist>> GetArtists()
			=> db.Artists
			.Include(t => t.Albums)
			.ThenInclude(a => a.Tracks)
			.AsNoTracking()
			.ToList();

        [HttpGet("{name}")]
        public async Task<ActionResult<Track>> GetArtistByName(string name) {
	        var artist = await db.Artists
		        .Include(t => t.Albums)
		        .ThenInclude(a => a.Tracks)
				.FirstOrDefaultAsync(a => a.Name == name);
            return artist is null ? NotFound() : Ok(artist);
        }
    }
}
