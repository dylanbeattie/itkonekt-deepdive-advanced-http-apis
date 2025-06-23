using System.Text.Json.Serialization;

namespace Rocklist.Data.Entities;

public class Track(Guid id, string title) {
	public Guid Id { get; set; } = id;
	// public Guid ArtistId { get; set; } = artistId;
	public string Title { get; set; } = title;
	[JsonIgnore]
	public Artist Artist { get; set; } = null!;

	[JsonIgnore]
	public Album Album { get; set; } = null!;

	public Track(Guid id, string title, Artist artist) : this(id, title) {
		this.Artist = artist;
	}

	public override string ToString() => $"{Artist?.Name}: {Title}";
}