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
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.IdAuditLog);
            builder.Property(a => a.Action).IsRequired().HasMaxLength(100);
            builder.Property(a => a.TableName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.RecordId).IsRequired();
            builder.Property(a => a.OldValues);
            builder.Property(a => a.NewValues);
            builder.Property(a => a.Description);
            builder.Property(a => a.CreatedAt).HasColumnType("datetime");
        }
    }
}
