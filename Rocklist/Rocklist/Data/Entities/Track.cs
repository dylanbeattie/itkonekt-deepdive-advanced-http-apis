using System.Text.Json.Serialization;

namespace Rocklist.Data.Entities {
	public class Artist {
		public string Name { get; set; } = String.Empty;
		public List<Album> Albums { get; set; } = [];
		public override string ToString() => Name;
	}

	public class Album {
		public string ArtistName { get; set; } = String.Empty;
		[JsonIgnore]
		public Artist Artist { get; set; } = null!;
		public string Title { get; set; } = String.Empty;
		public int Year { get; set; }
		public List<Track> Tracks { get; set; } = [];
		public override string ToString() => Title;
	}

	public class Track {
		[JsonIgnore]
		public string FilePath { get; set; } = String.Empty;
		public string ArtistName { get; set; } = String.Empty;
		public string Title { get; set; } = String.Empty;

		[JsonIgnore]
		public Artist Artist { get; set; } = null!;

		[JsonIgnore]
		public Album Album { get; set; } = null!;

		public override string ToString() => $"{TrackNumber}: {Title}";

	}
}
