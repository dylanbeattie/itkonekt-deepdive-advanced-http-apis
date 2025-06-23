using System.Text.Json.Serialization;

namespace Rocklist.Data.Entities;

public class Album(Guid id, string name, int year) {
	public Guid Id { get; set; } = id;

	[JsonIgnore]
	public Artist Artist { get; set; } = null!;

	public string Name { get; set; } = name;
	public int Year { get; set; } = year;
	public List<Track> Tracks { get; set; } = [];

	public Album(Guid id, Artist artist, string name, int year, IEnumerable<Track> tracks) : this(id, name, year) {
		this.Artist = artist;
		this.Tracks = [..tracks];
		foreach (var track in this.Tracks) track.Album = this;
	}
	public override string ToString() => $"{Artist?.Name}: {Name}";
}