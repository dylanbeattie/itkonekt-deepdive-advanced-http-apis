using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;
using Rocklist.Data.Entities;

namespace Rocklist.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController(RocklistDbContext db) : ControllerBase {
        [HttpGet]
        public ActionResult<IEnumerable<Track>> GetAll()
			=> db.Tracks
			.Include(t => t.Album)
			.Include(t => t.Artist)
			.AsNoTracking()
			.ToList();

        [HttpGet("{artist}/{album}/{title}")]
        public async Task<ActionResult<Track>> GetByFilePathAsync(string artist, string album, string title) {
			var track = await db.Tracks.FirstOrDefaultAsync(t => t.ArtistName == artist && t.AlbumName == album && t.Title == title);
            return track is null ? NotFound() : track;
        }
    }
}
