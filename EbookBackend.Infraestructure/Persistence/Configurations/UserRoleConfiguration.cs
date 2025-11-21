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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(ur => new { ur.IdUser, ur.IdRole });

            builder.HasOne(ur => ur.UserObj)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.IdUser);

            builder.HasOne(ur => ur.RoleObj)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.IdRole);
        }
    }
}
