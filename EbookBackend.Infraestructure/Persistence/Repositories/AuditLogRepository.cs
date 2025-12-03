using EbookBackend.Domain.Entities;
using EbookBackend.Infraestructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Repositories
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        private readonly EbookStoreDbContext _context;

        public AuditLogRepository(EbookStoreDbContext context) : base(context) {
            _context = context;
        }
    }
}
