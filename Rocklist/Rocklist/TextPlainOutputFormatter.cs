using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Rocklist.Data.Entities;

namespace Rocklist;

public class TextPlainOutputFormatter : TextOutputFormatter {
	public TextPlainOutputFormatter() {
		SupportedMediaTypes.Add("text/plain");
		SupportedEncodings.Add(Encoding.UTF8);
		SupportedEncodings.Add(Encoding.Unicode);
	}

	public override async Task WriteResponseBodyAsync(
		OutputFormatterWriteContext context,
		Encoding selectedEncoding) {
		var track = (Track) context.Object;
		var content = $"""
		               {track.Artist.Name}: {track.Title}

		               {track.Lyrics}
		               """;
		await context.HttpContext.Response.WriteAsync(content);
	}
}