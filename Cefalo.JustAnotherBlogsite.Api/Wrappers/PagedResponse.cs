﻿namespace Cefalo.JustAnotherBlogsite.Api.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = String.Empty;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
