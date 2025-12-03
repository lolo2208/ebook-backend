using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Books, unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _unitOfWork.Books.GetBooksByGenreAsync(genreId);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorAsync()
        {
            return await _unitOfWork.Books.GetBooksWithAuthorAsync();
        }

        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _unitOfWork.Books.GetByISBNAsync(isbn);
        }
    }
}
