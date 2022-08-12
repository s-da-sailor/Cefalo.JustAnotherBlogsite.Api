using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Cefalo.JustAnotherBlogsite.Api.Utils.Formatters
{
    public class UserXmlOutputFormatter : TextOutputFormatter
    {
        public UserXmlOutputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
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

                buffer.AppendLine("<? xml version =\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?>");
                buffer.AppendLine("<root>");
                buffer.AppendLine($"<pageNumber>{contextObject.PageNumber}</pageNumber>");
                buffer.AppendLine($"<pageSize>{contextObject.PageSize}</pageSize>");
                buffer.AppendLine($"<totalRecords>{contextObject.TotalRecords}</totalRecords>");
                buffer.AppendLine($"<data>");
                foreach (UserDetailsDto user in users)
                {
                    ConvertToXmlUser(buffer, user);
                }
                buffer.AppendLine($"</data>");

                buffer.AppendLine($"<succeeded>{contextObject.Succeeded}</succeeded>");
                buffer.AppendLine($"<errors>{contextObject.Errors?.ToString()}</errors>");
                buffer.AppendLine($"<message>{contextObject.Message}</message>");
                buffer.AppendLine("</root>");
            }
            else if(context.Object is Response<UserDetailsDto>)
            {
                var contextObject = (Response<UserDetailsDto>)context.Object;
                var user = contextObject.Data;
                buffer.AppendLine("<? xml version =\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?>");
                buffer.AppendLine("<root>");
                buffer.AppendLine($"<data>");
                ConvertToXmlUser(buffer, user);
                buffer.AppendLine($"</data>");

                buffer.AppendLine($"<succeeded>{contextObject.Succeeded}</succeeded>");
                buffer.AppendLine($"<errors>{contextObject.Errors?.ToString()}</errors>");
                buffer.AppendLine($"<message>{contextObject.Message}</message>");
                buffer.AppendLine("</root>");
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToXmlUser(StringBuilder buffer, UserDetailsDto user)
        {
            buffer.AppendLine("<user>");
            buffer.AppendLine($"<userid>{user.UserId}</userid>");
            buffer.AppendLine($"<username>{user.Username}</username>");
            buffer.AppendLine($"<fullname>{user.FullName}</fullname>");
            buffer.AppendLine($"<email>{user.Email}</email>");
            buffer.AppendLine($"<role>{user.Role}</role>");
            buffer.AppendLine($"<createdAt>{user.CreatedAt}</createdAt>");
            buffer.AppendLine($"<updatedAt>{user.UpdatedAt}</updatedAt>");
            buffer.AppendLine($"<passwordChangedAt>{user.PasswordChangedAt}</passwordChangedAt>");
            buffer.AppendLine("</user>");
        }
    }
}
