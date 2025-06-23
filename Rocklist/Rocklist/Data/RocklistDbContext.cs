using Microsoft.EntityFrameworkCore;
using Rocklist.Data.Entities;

namespace Rocklist.Data;

public class RocklistDbContext(DbContextOptions<RocklistDbContext> options, Library library) : DbContext(options) {
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Album> Albums { get; set; }
	public DbSet<Track> Tracks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Artist: Name is the key
		modelBuilder.Entity<Artist>()
			.HasKey(a => a.Name);
		modelBuilder.Entity<Artist>()
			.HasMany(a => a.Albums)
			.WithOne(a => a.Artist)
			.HasForeignKey(a => a.ArtistName);

		// Album: composite key (ArtistName, Title)
		modelBuilder.Entity<Album>()
			.HasKey(a => new { a.ArtistName, a.Title });
		modelBuilder.Entity<Album>()
			.HasMany(a => a.Tracks)
			.WithOne(t => t.Album)
			.HasForeignKey(t => new { t.ArtistName, t.AlbumName });
		modelBuilder.Entity<Album>()
			.HasOne(a => a.Artist)
			.WithMany(a => a.Albums)
			.HasForeignKey(a => a.ArtistName);

		// Track: FilePath is the key
		modelBuilder.Entity<Track>()
			.HasKey(t => t.FilePath);
		modelBuilder.Entity<Track>()
			.HasOne(t => t.Artist)
			.WithMany()
			.HasForeignKey(t => t.ArtistName);
		modelBuilder.Entity<Track>()
			.HasOne(t => t.Album)
			.WithMany(a => a.Tracks)
			.HasForeignKey(t => new { t.ArtistName, t.AlbumName });

		modelBuilder.Entity<Artist>().HasData(library.Artists);
		modelBuilder.Entity<Album>().HasData(library.Albums);
		modelBuilder.Entity<Track>().HasData(library.Tracks);
	}

}