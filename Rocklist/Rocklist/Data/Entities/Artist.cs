namespace Rocklist.Data.Entities;

public class Artist(Guid id, string name, string? slug = null) {
	public Guid Id { get; set; } = id;
	public string Name { get; set; } = name;
	public List<Album> Albums { get; set; } = [];

	public string Slug { get; set; } = slug ?? name.Slugify();

	public override string ToString() => Name;
}