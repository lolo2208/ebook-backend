using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class Publisher
    {
        public int IdPublisher { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<Book>? Books { get; set; }
    }
}
