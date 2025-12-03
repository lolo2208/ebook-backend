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
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Authors, unitOfWork)
        {
        }
    }
}
