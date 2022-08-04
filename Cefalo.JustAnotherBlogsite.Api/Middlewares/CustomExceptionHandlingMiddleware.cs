using Cefalo.JustAnotherBlogsite.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Cefalo.JustAnotherBlogsite.Api.Middlewares
{
    public static class CustomExceptionHandlingMiddleware
    {
        public static IApplicationBuilder CustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    var error = exceptionHandlerPathFeature?.Error;
                    var errorMessage = "Something went wrong";
                    var errorStatusCode = (int)HttpStatusCode.InternalServerError;

                    switch (error)
                    {
                        case NotFoundException:
                            errorMessage = error.Message;
                            errorStatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        case UnauthorizedException:
                            errorMessage = error.Message;
                            errorStatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        case ForbiddenException:
                            errorMessage = error.Message;
                            errorStatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        case BadRequestException:
                            errorMessage = error.Message;
                            errorStatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                    }

                    context.Response.ContentType = Text.Plain;
                    context.Response.StatusCode = errorStatusCode;
                    await context.Response.WriteAsync(errorMessage);
                });
            });
            return app;
        }
    }
}