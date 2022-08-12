using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class UserHtmlOutputFormatter : TextOutputFormatter
    {
        public UserHtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(Response<UserDetailsDto>).IsAssignableFrom(type) || typeof(PagedResponse<List<UserDetailsDto>>).IsAssignableFrom(type))
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

            if (context.Object is PagedResponse<List<UserDetailsDto>>)
            {
                PagedResponse<List<UserDetailsDto>> contextObject = (PagedResponse<List<UserDetailsDto>>)context.Object;
                var users = contextObject.Data;

                buffer.AppendLine($"<h3>pageNumber : {contextObject.PageNumber}</h3>");
                buffer.AppendLine($"<h3>pageSize : {contextObject.PageSize}</h3>");
                buffer.AppendLine($"<h3>totalRecords : {contextObject.TotalRecords}</h3>");
                buffer.AppendLine($"<br>");

                foreach (UserDetailsDto user in users)
                {
                    ConvertToHtmlUser(buffer, user);
                }

                buffer.AppendLine($"<h3>succeeded : {contextObject.Succeeded}</h3>");
                buffer.AppendLine($"<h3>errors : {contextObject.Errors?.ToString()}</h3>");
                buffer.AppendLine($"<h3>message : {contextObject.Message}</h3>");
            }
            else if (context.Object is Response<UserDetailsDto>)
            {
                var contextObject = (Response<UserDetailsDto>)context.Object;
                var user = contextObject.Data;

                ConvertToHtmlUser(buffer, user);

                buffer.AppendLine($"<h3>succeeded : {contextObject.Succeeded}</h3>");
                buffer.AppendLine($"<h3>errors : {contextObject.Errors?.ToString()}</h3>");
                buffer.AppendLine($"<h3>message : {contextObject.Message}</h3>");
            }
            buffer.AppendLine("</body>");
            buffer.AppendLine("</html>");

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToHtmlUser(StringBuilder buffer, UserDetailsDto user)
        {
            buffer.AppendLine($"<div>");
            buffer.AppendLine($"<h2>userid : {user.UserId}</h2>");
            buffer.AppendLine($"<h2>username : {user.Username}</h2>");
            buffer.AppendLine($"<h2>fullname : {user.FullName}</h2>");
            buffer.AppendLine($"<h2>email : {user.Email}</h2>");
            buffer.AppendLine($"<h2>role : {user.Role}</h2>");
            buffer.AppendLine($"<h2>createdAt : {user.CreatedAt}</h2>");
            buffer.AppendLine($"<h2>updatedAt : {user.UpdatedAt}</h2>");
            buffer.AppendLine($"<h2>passwordChangedAt : {user.PasswordChangedAt}</h2>");
            buffer.AppendLine($"</div>");
            buffer.AppendLine($"<br>");
        }
    }
}
