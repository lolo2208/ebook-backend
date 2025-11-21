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
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.ToTable("BookReviews", t =>
            {
                t.HasCheckConstraint("CK_BookReviews_Rating", "RatingValue BETWEEN 1 AND 5");
            });
            builder.HasKey(br => br.IdBookReview);

            builder.Property(br => br.RatingValue).IsRequired();

            builder.HasOne(br => br.User)
                   .WithMany(u => u.BookReviews)
                   .HasForeignKey(br => br.IdUser)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(br => br.Book)
                   .WithMany(b => b.Reviews)
                   .HasForeignKey(br => br.IdBook)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
