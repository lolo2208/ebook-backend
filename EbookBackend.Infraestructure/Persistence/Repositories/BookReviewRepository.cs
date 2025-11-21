using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Repositories
{
    public class BookReviewRepository : Repository<BookReview>, IBookReviewRepository
    {
        private readonly EbookStoreDbContext _context;

        public BookReviewRepository(EbookStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
