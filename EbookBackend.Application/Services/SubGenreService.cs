using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class SubGenreService : BaseService<SubGenre>, ISubGenreService
    {
        public SubGenreService(ISubGenreRepository subGenreRepository) : base(subGenreRepository) { }
    }
}
