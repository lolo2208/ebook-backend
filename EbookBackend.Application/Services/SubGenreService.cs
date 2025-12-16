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
    public class SubGenreService : ISubGenreService
    {
        public SubGenreService(IUnitOfWork unitOfWork, IMapper mapper) { }
    }
}
