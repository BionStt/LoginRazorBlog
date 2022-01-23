using LoginRazorBlog.Domain;
using LoginRazorBlog.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Infrastructure.Configuration
{
    public class LocalAccountConfiguration : IEntityTypeConfiguration<LocalAccountEntity>
    {
        public void Configure(EntityTypeBuilder<LocalAccountEntity> builder)
        {
            builder.ToTable("LocalAccount");
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Username).HasMaxLength(32);
            builder.Property(e => e.PasswordHash).HasMaxLength(64);
            builder.Property(e => e.LastLoginIp).HasMaxLength(64);
            builder.Property(e => e.CreateTimeUtc).HasColumnType("datetime");
            builder.Property(e => e.LastLoginTimeUtc).HasColumnType("datetime");

            builder.HasData(
                new LocalAccountEntity
                {
                    Id = Guid.NewGuid(),
                    Username="Admin",
                    //  PasswordHash="Test@45677889988",
                    PasswordHash = Helper.HashPassword("Test@45677889988"),
                    CreateTimeUtc =DateTime.Now

                });
        }
    }
}
