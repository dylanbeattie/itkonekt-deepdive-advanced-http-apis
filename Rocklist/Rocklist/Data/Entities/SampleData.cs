namespace Rocklist.Data.Entities;

public static class SampleData {
	public static class Artists {
		public static Artist AcDc = new(TestGuid.Next(), "AC/DC");
		public static Artist BonJovi = new(TestGuid.Next(), "Bon Jovi");
		public static Artist Cinderella = new(TestGuid.Next(), "Cinderella");
		public static Artist DefLeppard = new(TestGuid.Next(), "Def Leppard");
		public static Artist Europe = new(TestGuid.Next(), "Europe");
		public static Artist Motorhead = new(TestGuid.Next(), "Motörhead");
		public static Artist[] AllArtists = [AcDc, BonJovi, Cinderella, DefLeppard, Europe, Motorhead];
	}

	public static class Albums {
		private static Album Album(Guid id, Artist artist, string name, int year, string[] trackTitles) =>
			new(id, artist, name, year, name.Slugify(), trackTitles.Select(title => new Track(TestGuid.Next(), title, title.Slugify(), artist)));

		public static Album AcDcHighwayToHell = Album(TestGuid.Next(), Artists.AcDc, "Highway to Hell", 1979, [
			"Highway to Hell", "Girls Got Rhythm", "Walk All Over You", "Touch Too Much", "Beating Around the Bush",
			"Shot Down in Flames", "Get It Hot", "If You Want Blood (You've Got It)", "Love Hungry Man", "Night Prowler"
		]);

		public static Album AcdcBackInBlack = Album(TestGuid.Next(), Artists.AcDc, "Back in Black", 1980, [
			"Hells Bells", "Shoot to Thrill", "What Do You Do For Money Honey", "Givin' the Dog a Bone", "Let Me Put My Love Into You",
			"Back in Black", "You Shook Me All Night Long", "Have A Drink on Me", "Shake a Leg", "Rock'n'Roll Ain't Noise Pollution"
		]);

		public static Album BonJoviBonJovi = Album(TestGuid.Next(), Artists.BonJovi, "Bon Jovi", 1984, [
			"Runaway", "Roulette", "She Don't Know Me", "Shot Through the Heart", "Love Lies",
			"Breakout", "Burning for Love", "Come Back", "Get Ready"
		]);

		public static Album BonJovi7800Fahrenheit = Album(TestGuid.Next(), Artists.BonJovi, "7800 ° Fahrenheit", 1985, [
			"In and Out of Love", "The Price of Love", "Only Lonely", "King of the Mountain",
			"Silent Night", "Tokyo Road", "The Hardest Part Is the Night", "Always Run to You",
			"(I Don't Wanna Fall) To the Fire", "Secret Dreams"
		]);

		public static Album BonJoviSlipperyWhenWet = Album(TestGuid.Next(), Artists.BonJovi, "Slippery When Wet", 1986, [
			"Let It Rock", "You Give Love a Bad Name", "Livin' on a Prayer", "Social Disease", "Wanted Dead or Alive",
			"Raise Your Hands", "Without Love", "I'd Die for You", "Never Say Goodbye", "Wild in the Streets"
		]);

		public static Album BonJoviNewJersey = Album(TestGuid.Next(), Artists.BonJovi, "New Jersey", 1988, [
			"Lay Your Hands on Me", "Bad Medicine", "Born to Be My Baby", "Living in Sin", "Blood on Blood",
			"Homebound Train", "Wild Is the Wind", "Ride Cowboy Ride", "Stick to Your Guns", "I'll Be There for You",
			"99 in the Shade", "Love for Sale"
		]);

		public static Album DefLeppardHysteria = Album(TestGuid.Next(), Artists.DefLeppard, "Hysteria", 1987, [
			"Women", "Rocket", "Animal", "Love Bites", "Pour Some Sugar on Me", "Armageddon It",
			"Gods of War", "Don't Shoot Shotgun", "Run Riot", "Hysteria", "Excitable", "Love and Affection"
		]);

		public static Album DefLeppardPyromania = Album(TestGuid.Next(), Artists.DefLeppard, "Pyromania", 1983, [
			"Rock! Rock! (Till You Drop)", "Photograph", "Stagefright", "Too Late for Love", "Die Hard the Hunter",
			"Foolin'", "Rock of Ages", "Comin' Under Fire", "Action! Not Words!", "Billy's Got a Gun"
		]);

		public static Album EuropeTheFinalCountdown = Album(TestGuid.Next(), Artists.Europe, "The Final Countdown", 1986, [
			"The Final Countdown", "Rock the Night", "Carrie", "Danger on the Track", "Ninja",
			"Cherokee", "Time Has Come", "Heart of Stone", "On the Loose", "Love Chaser"
		]);

		public static Album EuropeLastLookAtEden = Album(TestGuid.Next(), Artists.Europe, "Last Look at Eden", 2009, [
			"Prelude", "Last Look at Eden", "Gonna Get Ready", "Catch That Plane", "New Love in Town",
			"The Beast", "Mojito Girl", "No Stone Unturned", "Only Young Twice", "U Devil U",
			"Run with the Angels", "In My Time", "Sign of the Times (Live from Paris '05)"
		]);

		public static Album MotorheadAceOfSpades = Album(TestGuid.Next(), Artists.Motorhead, "Ace of Spades", 1980, [
			"Ace of Spades", "Love Me Like a Reptile", "Shoot You in the Back", "Live to Win", "Fast and Loose",
			"(We Are) the Road Crew", "Fire, Fire", "Jailbait", "Dance", "Bite the Bullet",
			"The Chase Is Better Than the Catch", "The Hammer"
		]);

		public static Album[] AllAlbums = [
			AcDcHighwayToHell, AcdcBackInBlack,
			BonJoviBonJovi, BonJovi7800Fahrenheit, BonJoviSlipperyWhenWet, BonJoviNewJersey,
			DefLeppardPyromania, DefLeppardHysteria,
			EuropeTheFinalCountdown, EuropeLastLookAtEden,
			MotorheadAceOfSpades
		];
	}

	public static class Tracks {
		public static IEnumerable<Track> AllTracks => Albums.AllAlbums.SelectMany(a => a.Tracks);
	}

	public static class TestGuid {
		private static int seed = 1;
		public static Guid Next() => Guid.Parse($"DEADBEEF-0000-0000-0000-{seed++:D12}");
	}
}

public static class SeedData {
	public static IEnumerable<object> For(IEnumerable<Artist> artists) => artists;
	public static IEnumerable<object> For(IEnumerable<Album> albums) => albums.Select(a =>
		new { Id = a.Id, Year = a.Year, ArtistId = a.Artist.Id, Name = a.Name, Slug = a.Slug });

	public static IEnumerable<object> For(IEnumerable<Track> tracks) => tracks.Select(t =>
		new { Id = t.Id, ArtistId = t.Artist.Id, AlbumId = t.Album.Id, Title = t.Title, Slug = t.Slug });
}