using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class Author
    {
        public int IdAuthor { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public string? About { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<Book>? Books { get; set; }
    }
}
