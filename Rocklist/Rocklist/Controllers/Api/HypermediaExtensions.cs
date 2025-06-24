using Rocklist.Data.Entities;

namespace Rocklist.Controllers.Api;

public static class HypermediaExtensions {
	public static object ToResource(this Artist artist) => new {
		 _links = new {
			 self = new { href = $"/api/artists/{artist.Slug}" },
			 albums = new { href = $"/api/artists/{artist.Slug}/albums" }
		 },
		 name = artist.Name
	};
}