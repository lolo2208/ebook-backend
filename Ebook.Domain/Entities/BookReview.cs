using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class BookReview
    {
        public int IdBookReview { get; set; }
        public int IdUser { get; set; }
        public int IdBook { get; set; }
        public int RatingValue { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
