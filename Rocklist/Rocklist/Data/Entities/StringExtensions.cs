using System.Text.RegularExpressions;

namespace Rocklist.Data.Entities;

public static class StringExtensions {
	public static string Slugify(this string s) => Regex.Replace(s.ToLowerInvariant(), @"\s+", "-");
}
