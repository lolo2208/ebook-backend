using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class Genre
    {
        public int IdGenre { get; set; }
        public string GenreName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<SubGenre>? SubGenres { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
