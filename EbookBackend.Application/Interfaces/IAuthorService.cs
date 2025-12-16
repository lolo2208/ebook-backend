using EbookBackend.Application.Dto;
using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto> SaveAuthor(AuthorDto authorDto);
        Task<AuthorDto> GetAuthorById(int idAuhtor);
        Task<IEnumerable<AuthorDto>> GetAuthorByQuery(string query);
    }
}
