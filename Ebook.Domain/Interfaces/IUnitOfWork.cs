using EbookBackend.Infraestructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAuditLogRepository AuditLogs { get; }
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IBookReviewRepository BookReviews { get; }
        IGenreRepository Genres { get; }
        IPublisherRepository Publishers { get; }
        IRoleRepository Roles { get; }
        ISubGenreRepository SubGenres { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IUserTokenRepository UserTokens { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
