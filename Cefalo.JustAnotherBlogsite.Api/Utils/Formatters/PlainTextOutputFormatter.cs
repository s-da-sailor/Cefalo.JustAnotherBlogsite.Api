using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class PlainTextOutputFormatter : TextOutputFormatter
    {
        public PlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
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

            buffer.AppendLine("status : success");
            buffer.AppendLine();

            if (context.Object is IEnumerable<BlogDetailsDto>)
            {
                IEnumerable<BlogDetailsDto> posts = (IEnumerable<BlogDetailsDto>)context.Object;

                foreach (BlogDetailsDto post in posts)
                {
                    ConvertToPlainText(buffer, post);
                }
            }
            else
            {
                ConvertToPlainText(buffer, (BlogDetailsDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToPlainText(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine($"id : {post.BlogId}");
            buffer.AppendLine($"title : \"{post.Title}\"");
            buffer.AppendLine($"description : \"{post.Description}\"");
            buffer.AppendLine($"author: \"{post.AuthorUsername}\"");
            buffer.AppendLine($"createdAt : \"{post.CreatedAt}\"");
            buffer.AppendLine($"updatedAt : \"{post.UpdatedAt}\"");
            buffer.AppendLine();
        }
    }
}
