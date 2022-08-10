using Cefalo.JustAnotherBlogsite.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.UserId);

            builder.HasIndex(b => b.UserId).IsUnique();
            builder.Property(b => b.UserId).IsRequired();

            builder.HasIndex(b => b.Username).IsUnique();
            builder.Property(b => b.Username).IsRequired().HasMaxLength(30);

            builder.Property(b => b.FullName).IsRequired().HasMaxLength(50);

            builder.HasIndex(b => b.Email).IsUnique();
            builder.Property(b => b.Email).IsRequired().HasMaxLength(320);

            builder.Property(b => b.PasswordHash).IsRequired().HasMaxLength(500);

            builder.Property(b => b.PasswordSalt).IsRequired().HasMaxLength(500);

            builder.Property(b => b.Role).IsRequired();

            builder.Property(b => b.CreatedAt).IsRequired();

            builder.Property(b => b.UpdatedAt).IsRequired();

            builder.Property(b => b.PasswordChangedAt).IsRequired();
        }
    }
}
