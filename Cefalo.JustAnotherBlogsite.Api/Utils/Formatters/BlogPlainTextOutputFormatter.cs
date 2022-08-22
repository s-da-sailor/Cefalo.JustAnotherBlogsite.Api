using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class BlogPlainTextOutputFormatter : TextOutputFormatter
    {
        public BlogPlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
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

                buffer.AppendLine($"pageNumber : {contextObject.PageNumber}");
                buffer.AppendLine($"pageSize : {contextObject.PageSize}");
                buffer.AppendLine($"totalRecords : {contextObject.TotalRecords}");
                buffer.AppendLine();

                foreach (BlogDetailsDto blog in blogs)
                {
                    ConvertToPlainTextBlog(buffer, blog);
                }

                buffer.AppendLine($"succeeded : {contextObject.Succeeded}");
                buffer.AppendLine($"errors : {contextObject.Errors?.ToString()}");
                buffer.AppendLine($"message : {contextObject.Message}");
            }
            else if (context.Object is Response<BlogDetailsDto>)
            {
                var contextObject = (Response<BlogDetailsDto>)context.Object;
                var blog = contextObject.Data;

                ConvertToPlainTextBlog(buffer, blog);

                buffer.AppendLine($"succeeded : {contextObject.Succeeded}");
                buffer.AppendLine($"errors : {contextObject.Errors?.ToString()}");
                buffer.AppendLine($"message : {contextObject.Message}");
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToPlainTextBlog(StringBuilder buffer, BlogDetailsDto post)
        {
            buffer.AppendLine($"blogId : {post.BlogId}");
            buffer.AppendLine($"title : \"{post.Title}\"");
            buffer.AppendLine($"description : \"{post.Description}\"");
            buffer.AppendLine($"authorId: {post.AuthorId}");
            buffer.AppendLine($"authorUsername: \"{post.AuthorUsername}\"");
            buffer.AppendLine($"createdAt : \"{post.CreatedAt}\"");
            buffer.AppendLine($"updatedAt : \"{post.UpdatedAt}\"");
            buffer.AppendLine();
        }
    }
}
