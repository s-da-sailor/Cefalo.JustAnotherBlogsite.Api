﻿namespace Cefalo.JustAnotherBlogsite.Api.Wrappers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public string Message { get; set; }
        public Response() { }

        public Response(T data)
        {
            Succeeded = true;
            Message = String.Empty;
            Errors = null;
            Data = data;
        }
    }
}