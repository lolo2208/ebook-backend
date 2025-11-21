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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.IdBook);

            builder.Property(b => b.Title).IsRequired().HasMaxLength(255);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(200);
            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(13);
            builder.Property(b => b.Price).HasColumnType("decimal(19,4)");
            builder.Property(b => b.AvailableUnits).HasDefaultValue(0);

            builder.HasOne(b => b.AuthorObj)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.IdAuthor)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.PublisherObj)
                   .WithMany(p => p.Books)
                   .HasForeignKey(b => b.IdPublisher)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.GenreObj)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.IdGenre)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.SubGenreObj)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.IdSubGenre)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
