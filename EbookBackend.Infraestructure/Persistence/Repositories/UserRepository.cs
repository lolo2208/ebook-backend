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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly EbookStoreDbContext _context;

        public UserRepository(EbookStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetByEmailWithRoleAsync(string email)
        {
            return _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
