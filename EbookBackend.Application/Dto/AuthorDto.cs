using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class AuthorDto
    {
        public int? IdAuthor { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorLastName {  get; set; }
        public string? About { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<BookDto>? Books { get; set; }
    }
}
