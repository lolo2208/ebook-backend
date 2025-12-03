using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EbookStoreDbContext _context;
        private IDbContextTransaction? _transaction;

        public IAuditLogRepository AuditLogs { get; }
        public IAuthorRepository Authors { get; }
        public IBookRepository Books { get; }
        public IBookReviewRepository BookReviews { get; } 
        public IGenreRepository Genres { get; }
        public IPublisherRepository Publishers { get; }
        public IRoleRepository Roles { get; }
        public ISubGenreRepository SubGenres { get; }
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IUserTokenRepository UserTokens { get; }

        public UnitOfWork(
            EbookStoreDbContext context,
            IAuditLogRepository auditLogs,
            IAuthorRepository authors,
            IBookRepository books,
            IBookReviewRepository bookReviews,
            IGenreRepository genres,
            IPublisherRepository publishers,
            IRoleRepository roles,
            ISubGenreRepository subGenres,
            IUserRepository users,
            IUserRoleRepository userRoles,
            IUserTokenRepository userTokens)
        {
            _context = context;
            AuditLogs = auditLogs;
            Authors = authors;
            Books = books;
            BookReviews = bookReviews;
            Genres = genres;
            Publishers = publishers;
            Roles = roles;
            SubGenres = subGenres;
            Users = users;
            UserRoles = userRoles;
            UserTokens = userTokens;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction!.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
