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

	public string Lyrics => """
	                        [Verse 1]
	                        A wild ride, over stony ground
	                        Such a lust for life, the circus comes to town
	                        We are the hungry ones, on a lightning raid
	                        Just like a river runs, like a fire needs flame
	                        Oh, I burn for you

	                        [Chorus]
	                        I got to feel it in my blood, whoa, oh
	                        I need your touch don't need your love, whoa, oh
	                        And I want, and I need, and I lust, animal
	                        And I want, and I need, and I lust, animal

	                        [Verse 2]
	                        I cry wolf, given mouth-to-mouth
	                        Like a moving heartbeat, in the witching hour
	                        I'm running with the wind, a shadow in the dust
	                        And like the driving rain, hey, like the restless rust
	                        I never sleep

	                        [Chorus]
	                        I got to feel it in my blood, whoa, oh
	                        I need your touch don't need your love, whoa, oh
	                        And I want, and I need, and I lust, animal
	                        And I want, and I need, and I lust, animal

	                        """;

}