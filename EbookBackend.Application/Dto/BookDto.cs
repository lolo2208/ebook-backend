using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class BookDto
    {
        public int? IdBook { get; set; }
        public int? IdAuthor { get; set; }
        public int? IdPublisher { get; set; }
        public int? IdGenre { get; set; }
        public int? IdSubGenre { get; set; }

        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Synopsis { get; set; }
        public string? Imprint { get; set; }
        public string? Language { get; set; }
        public string? ISBN { get; set; } = string.Empty;
        public int? AvailableUnits { get; set; }
        public decimal? Price { get; set; }
        public string? CoverImg { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
