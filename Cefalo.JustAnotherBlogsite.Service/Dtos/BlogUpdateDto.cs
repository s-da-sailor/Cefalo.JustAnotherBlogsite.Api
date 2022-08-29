using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Dtos
{
    public class BlogUpdateDto
    {
        public BlogUpdateDto(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
