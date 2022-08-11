using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class XmlOutputFormatter : TextOutputFormatter
    {
        public XmlOutputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(BlogDetailsDto).IsAssignableFrom(type) || typeof(IEnumerable<BlogDetailsDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            buffer.AppendLine("<? xml version =\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?>");
            buffer.AppendLine("<root>");
            buffer.AppendLine("<status>success</status>");
            buffer.AppendLine("<data>");

            if (context.Object is IEnumerable<BlogDetailsDto>)
            {
                IEnumerable<BlogDetailsDto> posts = (IEnumerable<BlogDetailsDto>)context.Object;
                
                foreach(BlogDetailsDto post in posts)
                {
                    ConvertToXml(buffer, post);
                }
            } else
            {
                ConvertToXml(buffer, (BlogDetailsDto)context.Object);
            }

            buffer.AppendLine("</data>");
            buffer.AppendLine("</root>");

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToXml(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine($"<id>{post.BlogId}</id>");
            buffer.AppendLine($"<title>{post.Title}</title>");
            buffer.AppendLine($"<description>{post.Description}</description>");
            buffer.AppendLine($"<author>{post.AuthorUsername}</author>");
            buffer.AppendLine($"<createdAt>{post.CreatedAt}</createdAt>");
            buffer.AppendLine($"<updatedAt>{post.UpdatedAt}</updatedAt>");
        }
    }
}
