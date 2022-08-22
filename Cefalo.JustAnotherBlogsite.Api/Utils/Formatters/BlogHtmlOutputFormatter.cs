using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class BlogHtmlOutputFormatter : TextOutputFormatter
    {
        public BlogHtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
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

            buffer.AppendLine("<!DOCTYPE html>");
            buffer.AppendLine("<html>");
            buffer.AppendLine("<head>");
            buffer.AppendLine("<title>Just Another Blogsite</title>");
            buffer.AppendLine("</head>");
            buffer.AppendLine();

            buffer.AppendLine("<body>");

            if (context.Object is PagedResponse<List<BlogDetailsDto>>)
            {
                PagedResponse<List<BlogDetailsDto>> contextObject = (PagedResponse<List<BlogDetailsDto>>)context.Object;
                var blogs = contextObject.Data;

                buffer.AppendLine($"<h3>pageNumber : {contextObject.PageNumber}</h3>");
                buffer.AppendLine($"<h3>pageSize : {contextObject.PageSize}</h3>");
                buffer.AppendLine($"<h3>totalRecords : {contextObject.TotalRecords}</h3>");
                buffer.AppendLine($"<br>");

                foreach (BlogDetailsDto blog in blogs)
                {
                    ConvertToHtmlBlog(buffer, blog);
                }

                buffer.AppendLine($"<h3>succeeded : {contextObject.Succeeded}</h3>");
                buffer.AppendLine($"<h3>errors : {contextObject.Errors?.ToString()}</h3>");
                buffer.AppendLine($"<h3>message : {contextObject.Message}</h3>");
            }
            else if (context.Object is Response<BlogDetailsDto>)
            {
                var contextObject = (Response<BlogDetailsDto>)context.Object;
                var blog = contextObject.Data;

                ConvertToHtmlBlog(buffer, blog);

                buffer.AppendLine($"<h3>succeeded : {contextObject.Succeeded}</h3>");
                buffer.AppendLine($"<h3>errors : {contextObject.Errors?.ToString()}</h3>");
                buffer.AppendLine($"<h3>message : {contextObject.Message}</h3>");
            }
            buffer.AppendLine("</body>");
            buffer.AppendLine("</html>");

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToHtmlBlog(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine("<div>");
            buffer.AppendLine($"<h3>blogId : {post.BlogId}</h3>");
            buffer.AppendLine($"<h1>title : {post.Title}</h1>");
            buffer.AppendLine($"<p>description : {post.Description}</p>");
            buffer.AppendLine($"<h2>authorId: {post.AuthorId}</h2>");
            buffer.AppendLine($"<h2>authorUsername: {post.AuthorUsername}</h2>");
            buffer.AppendLine($"<h3>createdAt : {post.CreatedAt}</h3>");
            buffer.AppendLine($"<h3>updatedAt : {post.UpdatedAt}</h3>");
            buffer.AppendLine("</div>");
            buffer.AppendLine("<br>");
        }
    }
}
