using AutoMapper;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Infraestructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {

        }
    }
}
