using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class Book
    {
        public int IdBook { get; set; }
        public int IdAuthor { get; set; }
        public int IdPublisher { get; set; }
        public int IdGenre { get; set; }
        public int IdSubGenre { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public string? Imprint { get; set; }
        public string? Language { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int AvailableUnits { get; set; }
        public decimal Price { get; set; }
        public string? CoverImg { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Author AuthorObj { get; set; } = null!;
        public Publisher PublisherObj { get; set; } = null!;
        public Genre GenreObj { get; set; } = null!;
        public SubGenre SubGenreObj { get; set; } = null!;
        public ICollection<BookReview>? Reviews { get; set; }
    }
}
