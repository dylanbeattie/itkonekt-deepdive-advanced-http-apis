using Id3;
using Rocklist.Data.Entities;

//public class Library(string rootPath) {

//	public static Library FromPath(string rootPath) =>  new Library(rootPath).Scan();

//	private readonly Dictionary<string, Id3Tag> tags = [];

//	public Library Scan() {
//		foreach (var file in Directory.GetFiles(rootPath, "*.mp3", SearchOption.AllDirectories)) {
//			using var mp3 = new Mp3(file);
//			var tag = mp3.GetTag(Id3Version.V23);
//			if (tag != null) tags.Add(file, tag);
//		}
//		return this;
//	}

//	public IEnumerable<Track> Tracks => tags.Select(entry => new Track {
//		FilePath = entry.Key,
//		Title = entry.Value.Title.Value,
//		ArtistName = String.Join(", ", entry.Value.Artists.Value),
//	}).Where(track => track.Title != String.Empty && track.Title != null && track.AlbumName != null && track.AlbumName != String.Empty);

//	public IEnumerable<Artist> Artists
//		=> tags.Values.Select(t => String.Join(", ", t.Artists.Value)).Distinct().Select(name => new Artist { Name = name });

//	public IEnumerable<Album> Albums => tags.Values.GroupBy(t => (Album: t.Album.Value, Artist: String.Join(", ", t.Artists.Value)))
//		.Select(group => new Album { Title = group.Key.Album, ArtistName = group.Key.Artist })
//		.Where(album => !String.IsNullOrEmpty(album.Title));
//}
