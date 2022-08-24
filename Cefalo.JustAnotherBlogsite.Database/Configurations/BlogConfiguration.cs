using Cefalo.JustAnotherBlogsite.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Database.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.BlogId);

            builder.HasIndex(b => b.BlogId).IsUnique();
            builder.Property(b => b.BlogId).IsRequired();

            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);

            builder.Property(b => b.Description).IsRequired().HasMaxLength(10000);

            builder.Property(b => b.CreatedAt).IsRequired();

            builder.Property(b => b.UpdatedAt).IsRequired();

            builder.HasOne(s => s.Author).WithMany(s => s.Blogs).HasForeignKey(s => s.AuthorId);
        }
    }
}
