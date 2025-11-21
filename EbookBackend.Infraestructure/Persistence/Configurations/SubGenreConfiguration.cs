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
    public class SubGenreConfiguration : IEntityTypeConfiguration<SubGenre>
    {
        public void Configure(EntityTypeBuilder<SubGenre> builder)
        {
            builder.ToTable("SubGenres");
            builder.HasKey(s => s.IdSubGenre);
            builder.Property(s => s.SubGenreName).HasMaxLength(100);

            builder.HasOne(s => s.Genre)
                   .WithMany(g => g.SubGenres)
                   .HasForeignKey(s => s.IdGenre)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
