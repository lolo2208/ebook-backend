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
    public class BookService : BaseService<Book>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) : base(bookRepository) {
            _bookRepository = bookRepository;
        }
        
        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _bookRepository.GetBooksByGenreAsync(genreId);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorAsync()
        {
            return await _bookRepository.GetBooksWithAuthorAsync();
        }

        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _bookRepository.GetByISBNAsync(isbn);
        }
    }
}
