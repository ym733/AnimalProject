using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Text;

namespace Animal.Web.MediaComponents
{
	public static class FormHelper
	{
		public static IHtmlContent TextInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.LabelFor(expression, new { @class = "d-block"}).GetHtmlContentString());
			
			builder.Append(helper.TextBoxFor(expression, new { @class = "d-block" }).GetHtmlContentString());
			
			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent PasswordInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.LabelFor(expression, new { @class = "d-block" }).GetHtmlContentString());
			
			builder.Append(helper.PasswordFor(expression, new { @class = "d-block" }).GetHtmlContentString());
			

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent FileInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.LabelFor(expression, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.TextBoxFor(expression, new { @class = "d-block", @type="file" }).GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent EmailInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.LabelFor(expression, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.TextBoxFor(expression, new { @class = "d-block", @type = "email" }).GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent DateInput<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var builder = new StringBuilder();
			builder.Append(helper.ValidationMessageFor(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.LabelFor(expression, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.TextBoxFor(expression, new { @class = "d-block", @type = "date" }).GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent SingleStringInput<TModel>(this IHtmlHelper<TModel> helper, string expression)
		{
			var builder = new StringBuilder();

			builder.Append(helper.Label(null, expression, new { @class = "d-block" }).GetHtmlContentString());

			builder.Append(helper.TextBox(expression, null, new { @class = "d-block" }).GetHtmlContentString());

			return new HtmlString(builder.ToString());
		}
		public static IHtmlContent SubmitButton<TModel>(this IHtmlHelper<TModel> helper, string buttonText)
		{
			var button = new TagBuilder("input");
			button.Attributes.Add("type", "submit");
			button.Attributes.Add("value", buttonText);

			return new HtmlString(button.GetHtmlContentString());
		}
    }
}
