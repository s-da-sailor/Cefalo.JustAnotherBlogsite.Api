using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class UserPlainTextOutputFormatter : TextOutputFormatter
    {
        public UserPlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
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

            if (context.Object is PagedResponse<List<UserDetailsDto>>)
            {
                PagedResponse<List<UserDetailsDto>> contextObject = (PagedResponse<List<UserDetailsDto>>)context.Object;
                var users = contextObject.Data;

                buffer.AppendLine($"pageNumber : {contextObject.PageNumber}");
                buffer.AppendLine($"pageSize : {contextObject.PageSize}");
                buffer.AppendLine($"totalRecords : {contextObject.TotalRecords}");
                buffer.AppendLine();

                foreach (UserDetailsDto user in users)
                {
                    ConvertToPlainTextUser(buffer, user);
                }

                buffer.AppendLine($"succeeded : {contextObject.Succeeded}");
                buffer.AppendLine($"errors : {contextObject.Errors?.ToString()}");
                buffer.AppendLine($"message : {contextObject.Message}");
            }
            else if (context.Object is Response<UserDetailsDto>)
            {
                var contextObject = (Response<UserDetailsDto>)context.Object;
                var user = contextObject.Data;

                ConvertToPlainTextUser(buffer, user);

                buffer.AppendLine($"succeeded : {contextObject.Succeeded}");
                buffer.AppendLine($"errors : {contextObject.Errors?.ToString()}");
                buffer.AppendLine($"message : {contextObject.Message}");
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToPlainTextUser(StringBuilder buffer, UserDetailsDto user)
        {
            buffer.AppendLine($"userid : {user.UserId}");
            buffer.AppendLine($"username : \"{user.Username}\"");
            buffer.AppendLine($"fullname : \"{user.FullName}\"");
            buffer.AppendLine($"email : \"{user.Email}\"");
            buffer.AppendLine($"role : {user.Role}");
            buffer.AppendLine($"createdAt : \"{user.CreatedAt}\"");
            buffer.AppendLine($"updatedAt : \"{user.UpdatedAt}\"");
            buffer.AppendLine($"passwordChangedAt : \"{user.PasswordChangedAt}\"");
            buffer.AppendLine();
        }
    }
}
