using Cefalo.JustAnotherBlogsite.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Database.Models
{
    public class Blog
    {
        private Blog() { }
        public Blog(int blogId, string title, string description, DateTime createdAt, DateTime updatedAt, int authorId, User? author) 
            : this()
        {
            BlogId = blogId;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AuthorId = authorId;
            Author = author;
        }
        public int BlogId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
