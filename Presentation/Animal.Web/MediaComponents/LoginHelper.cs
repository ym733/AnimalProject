using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Text;

namespace Animal.Web.MediaComponents
{
	public static class LoginHelper
	{
		public static IHtmlContent TextInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			var div1 = new TagBuilder("div");
			div1.InnerHtml.Append(helper.LabelFor(expression).GetHtmlContentString());
			div1.InnerHtml.Append(": ");
			builder.Append(div1.GetHtmlContentString());

			var div2 = new TagBuilder("div");
			div2.InnerHtml.Append(helper.TextBoxFor(expression).GetHtmlContentString());
			builder.Append(div2.GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}

		public static IHtmlContent PasswordInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			var div1 = new TagBuilder("div");
			div1.InnerHtml.Append(helper.LabelFor(expression).GetHtmlContentString());
			div1.InnerHtml.Append(": ");
			builder.Append(div1.GetHtmlContentString());

			builder.Append(helper.PasswordFor(expression, new { @class = "d-block" }).GetHtmlContentString());
			

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent SubmitButton<TModel>(this IHtmlHelper<TModel> helper, string buttonText)
		{
			var builder = new StringBuilder();
			var div = new TagBuilder("div");
			builder.Append(div.GetHtmlContentString());

			var button = new TagBuilder("input");
			button.Attributes.Add("type", "submit");
			button.Attributes.Add("value", buttonText);

			builder.Append(button.GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}
	}
}
