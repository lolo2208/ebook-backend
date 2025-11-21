using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Repositories
{
    public class SubGenreRepository : Repository<SubGenre>, ISubGenreRepository
    {
        private readonly EbookStoreDbContext _context;

        public SubGenreRepository(EbookStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
