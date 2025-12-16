using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Interfaces
{
    public interface IBookService
    {
        Task<Book?> GetByISBNAsync(string isbn);
        Task<IEnumerable<Book>> GetBooksWithAuthorAsync();
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
    }
}
