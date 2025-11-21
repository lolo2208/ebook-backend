using EbookBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");

            builder.HasKey(ut => ut.IdUserToken);

            builder.Property(ut => ut.TokenHash).IsRequired().HasMaxLength(64);
            builder.Property(ut => ut.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(ut => ut.ExpiresAt).IsRequired();

            builder.HasOne(ut => ut.User)
                   .WithMany(u => u.UserTokens)
                   .HasForeignKey(ut => ut.IdUser);

            builder.HasIndex(ut => ut.TokenHash).IsUnique();
            builder.HasIndex(ut => ut.IdUser);
        }
    }
}
