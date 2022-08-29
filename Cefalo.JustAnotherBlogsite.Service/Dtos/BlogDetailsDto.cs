using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Dtos
{
    public class BlogDetailsDto
    {
        public BlogDetailsDto(int blogId, string title, string description, int authorId, string authorUsername, DateTime createdAt, DateTime updatedAt)
        {
            BlogId = blogId;
            Title = title;
            Description = description;
            AuthorId = authorId;
            AuthorUsername = authorUsername;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int BlogId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
