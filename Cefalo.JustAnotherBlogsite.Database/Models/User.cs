using Cefalo.JustAnotherBlogsite.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Cefalo.JustAnotherBlogsite.Api
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Role { get; set; } = 1;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime PasswordChangedAt { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
    }
}
  