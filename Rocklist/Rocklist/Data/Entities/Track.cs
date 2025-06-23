using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Rocklist.Data.Entities;

public class Track(Guid id, string title, string slug) {
	public Guid Id { get; set; } = id;

	public string Slug { get; set; } = slug;

	public string Title { get; set; } = title;
	[JsonIgnore]
	public Artist Artist { get; set; } = null!;

	[JsonIgnore]
	public Album Album { get; set; } = null!;

	public Track(Guid id, string title, string slug, Artist artist) : this(id, title, slug) {
		this.Artist = artist;
	}

	public override string ToString() => $"{Artist?.Name}: {Title}";
}