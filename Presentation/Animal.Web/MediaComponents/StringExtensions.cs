using Microsoft.AspNetCore.Html;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Animal.Web.MediaComponents
{
	public static class StringExtensions
	{
		public static string GetHtmlContentString(this IHtmlContent content)
		{
			using (var writer = new StringWriter())
			{
				content.WriteTo(writer, HtmlEncoder.Create(new[] { UnicodeRanges.BasicLatin }));
				return System.Net.WebUtility.HtmlDecode(writer.ToString());
			}
		}
	}
}
