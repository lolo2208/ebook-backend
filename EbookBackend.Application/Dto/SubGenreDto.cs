using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class SubGenreDto
    {
        public int? IdSubGenre { get; set; }
        public int? IdGenre { get; set; }
        public string? SubGenreName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
