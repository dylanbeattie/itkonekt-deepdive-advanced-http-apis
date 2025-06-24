using Microsoft.EntityFrameworkCore;
using Rocklist.Data.Entities;
using static Rocklist.Data.Entities.SampleData;

namespace Rocklist.Data;

public class RocklistDbContext(DbContextOptions<RocklistDbContext> options) : DbContext(options) {
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Album> Albums { get; set; }
	public DbSet<Track> Tracks { get; set; }

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
		configurationBuilder.Properties<string>().UseCollation("NOCASE");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Artist>(artist => {
			artist.Property(e => e.Name).HasMaxLength(200);
			artist.HasMany(a => a.Albums).WithOne(a => a.Artist);
			artist.HasIndex(a => a.Slug).IsUnique();
		});

		modelBuilder.Entity<Album>(album => {
			album.Property(a => a.Name).HasMaxLength(200);
			album.HasMany(a => a.Tracks).WithOne(t => t.Album);
			album.HasIndex(a => a.Slug);
		});

		modelBuilder.Entity<Track>(track => {
			track.Property(t => t.Title).HasMaxLength(200);
			track.HasIndex(t => t.Slug);
		});

		modelBuilder.Entity<Artist>().HasData(SeedData.For(SampleData.Artists.AllArtists));
		modelBuilder.Entity<Album>().HasData(SeedData.For(SampleData.Albums.AllAlbums));
		modelBuilder.Entity<Track>().HasData(SeedData.For(SampleData.Tracks.AllTracks));
	}

	public async Task<Artist?> FindArtistBySlug(string slug)
	 => await Artists.FirstOrDefaultAsync(a => a.Slug == slug);
}