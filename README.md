# itkonekt-deepdive-advanced-http-apis
ITkonekt .NET Deep Dive: Designing Advanced HTTP APIs

## Links:

**Content Negotiation:**

[https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-9.0](https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-9.0)

```csharp
public static class PropertyBuilderExtensions {
	private static StringSplitOptions sso = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
	public static string ToCsvString(this int[] ids) => String.Join(',', ids.Select(id => id.ToString()));

	private static int[] ToArray(this string ids) => (ids ?? "").Split(',', sso).Select(Int32.Parse).ToArray();

	private static readonly ValueComparer<int[]> comparer = new(
		(c1, c2) => c1.SequenceEqual(c2),
		c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
		c => c.ToArray());

	public static PropertyBuilder<int[]> WithArrayToCsvConvertor(this PropertyBuilder<int[]> t)
		=> t.HasConversion(ids => ids.ToCsvString(), ids => ids.ToArray(), comparer);


	public static string ToCsvString(this List<int> ids) => String.Join(',', ids.Select(id => id.ToString()));

	private static List<int> ToList(this string ids) => (ids ?? "").Split(',', sso).Select(Int32.Parse).ToList();

	private static readonly ValueComparer<List<int>> listComparer = new(
		(c1, c2) => c1.SequenceEqual(c2),
		c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
		c => c.ToList());

	public static PropertyBuilder<List<int>> WithListToCsvConvertor(this PropertyBuilder<List<int>> t)
		=> t.HasConversion(ids => ids.ToCsvString(), ids => ids.ToList(), listComparer);

	//public static string ToCsvString(this HashSet<int> ids) => String.Join(',', ids.Select(id => id.ToString()));

	//private static HashSet<int> ToHashSet(this string ids) => (ids ?? "").Split(',', sso).Select(Int32.Parse).ToHashSet();

	//private static readonly ValueComparer<HashSet<int>> hashSetComparer = new(
	//	(c1, c2) => c1.SequenceEqual(c2),
	//	c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
	//	c => c.ToHashSet());

	//public static PropertyBuilder<HashSet<int>> WithHashSetToCsvConvertor(this PropertyBuilder<HashSet<int>> t)
	//	=> t.HasConversion(ids => ids.ToCsvString(), ids => ids.ToHashSet(), hashSetComparer);


}
```

and use it with

```csharp
entity.Property(e => e.SomeListProperty).WithListToCsvConvertor().HasMaxLength(128).IsUnicode(false);
```

