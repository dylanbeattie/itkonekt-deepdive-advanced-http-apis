namespace Rocklist.Data.Entities;

public class Artist(Guid id, string name) {
	public Guid Id { get; set; } = id;
	public string Name { get; set; } = name;
	public List<Album> Albums { get; set; } = [];
	public override string ToString() => Name;
}