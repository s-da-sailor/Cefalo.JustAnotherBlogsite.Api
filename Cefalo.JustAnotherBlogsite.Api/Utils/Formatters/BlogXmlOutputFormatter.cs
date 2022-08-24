using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class BlogXmlOutputFormatter : TextOutputFormatter
    {
        public BlogXmlOutputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(Response<BlogDetailsDto>).IsAssignableFrom(type) || typeof(PagedResponse<List<BlogDetailsDto>>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is PagedResponse<List<BlogDetailsDto>>)
            {
                PagedResponse<List<BlogDetailsDto>> contextObject = (PagedResponse<List<BlogDetailsDto>>)context.Object;
                var blogs = contextObject.Data;

                buffer.AppendLine("<? xml version =\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?>");
                buffer.AppendLine("<root>");
                buffer.AppendLine($"<pageNumber>{contextObject.PageNumber}</pageNumber>");
                buffer.AppendLine($"<pageSize>{contextObject.PageSize}</pageSize>");
                buffer.AppendLine($"<totalRecords>{contextObject.TotalRecords}</totalRecords>");
                buffer.AppendLine($"<data>");
                foreach (BlogDetailsDto blog in blogs)
                {
                    ConvertToXmlBlog(buffer, blog);
                }
                buffer.AppendLine($"</data>");

                buffer.AppendLine($"<succeeded>{contextObject.Succeeded}</succeeded>");
                buffer.AppendLine($"<errors>{contextObject.Errors?.ToString()}</errors>");
                buffer.AppendLine($"<message>{contextObject.Message}</message>");
                buffer.AppendLine("</root>");
            }
            else if (context.Object is Response<BlogDetailsDto>)
            {
                var contextObject = (Response<BlogDetailsDto>)context.Object;
                var blog = contextObject.Data;
                buffer.AppendLine("<? xml version =\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?>");
                buffer.AppendLine("<root>");
                buffer.AppendLine($"<data>");
                ConvertToXmlBlog(buffer, blog);
                buffer.AppendLine($"</data>");

                buffer.AppendLine($"<succeeded>{contextObject.Succeeded}</succeeded>");
                buffer.AppendLine($"<errors>{contextObject.Errors?.ToString()}</errors>");
                buffer.AppendLine($"<message>{contextObject.Message}</message>");
                buffer.AppendLine("</root>");
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToXmlBlog(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine("<blog>");
            buffer.AppendLine($"<blogId>{post.BlogId}</blogId>");
            buffer.AppendLine($"<title>{post.Title}</title>");
            buffer.AppendLine($"<description>{post.Description}</description>");
            buffer.AppendLine($"<authorId>{post.AuthorId}</authorId>");
            buffer.AppendLine($"<authorUsername>{post.AuthorUsername}</authorUsername>");
            buffer.AppendLine($"<createdAt>{post.CreatedAt}</createdAt>");
            buffer.AppendLine($"<updatedAt>{post.UpdatedAt}</updatedAt>");
            buffer.AppendLine("</blog>");
        }
    }
}
