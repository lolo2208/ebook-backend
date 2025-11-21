using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly EbookStoreDbContext _context;

        public BookRepository(EbookStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorAsync()
        {
            return await _context.Books
                .Include(b => b.AuthorObj)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _context.Books
                .Where(b => b.IdGenre == genreId)
                .ToListAsync();
        }
    }
}
