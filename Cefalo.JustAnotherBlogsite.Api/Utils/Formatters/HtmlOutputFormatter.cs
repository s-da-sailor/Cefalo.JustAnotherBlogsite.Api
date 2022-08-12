using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class HtmlOutputFormatter : TextOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(BlogDetailsDto).IsAssignableFrom(type) || typeof(IEnumerable<BlogDetailsDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            buffer.AppendLine("<!DOCTYPE html>");
            buffer.AppendLine("<html>");
            buffer.AppendLine("<head>");
            buffer.AppendLine("<title>Just Another Blogsite</title>");
            buffer.AppendLine("</head>");
            buffer.AppendLine("<body>");
            buffer.AppendLine();

            if (context.Object is IEnumerable<BlogDetailsDto>)
            {
                IEnumerable<BlogDetailsDto> posts = (IEnumerable<BlogDetailsDto>)context.Object;

                foreach (BlogDetailsDto post in posts)
                {
                    ConvertToHtml(buffer, post);
                }
            }
            else
            {
                ConvertToHtml(buffer, (BlogDetailsDto)context.Object);
            }

            buffer.AppendLine("</body>");
            buffer.AppendLine("</html>");

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToHtml(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine("<div>");
            buffer.AppendLine($"<h3>id : {post.BlogId}</h2>");
            buffer.AppendLine($"<h1>title : {post.Title}</h1>");
            buffer.AppendLine($"<p>description : {post.Description}</p>");
            buffer.AppendLine($"<h2>author: {post.AuthorUsername}</h2>");
            buffer.AppendLine($"<h3>createdAt : {post.CreatedAt}</h3>");
            buffer.AppendLine($"<h3>updatedAt : {post.UpdatedAt}</h3>");
            buffer.AppendLine("</div>");
            buffer.AppendLine("<br>");
        }
    }
}
