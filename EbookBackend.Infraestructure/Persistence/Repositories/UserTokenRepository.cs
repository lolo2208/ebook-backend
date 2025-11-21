using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Persistence.Repositories
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        private readonly EbookStoreDbContext _context;
        public UserTokenRepository(EbookStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> CreateRefreshTokenAsync(int userId)
        {
            var token = GenerateRandomToken();

            var tokenHash = ComputeSha256Hash(token);

            var userToken = new UserToken
            {
                IdUser = userId,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.AddHours(8),
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<UserToken?> GetByTokenHashAsync(string tokenHash)
        {
            return await _context.UserTokens
                                 .FirstOrDefaultAsync(ut => ut.TokenHash == tokenHash && ut.IsActive);
        }

        public async Task<IEnumerable<UserToken>> GetByUserIdAsync(int userId)
        {
            return await _context.UserTokens
                                 .Where(ut => ut.IdUser == userId && ut.IsActive)
                                 .ToListAsync();
        }

        public async Task RevokeTokenAsync(string token)
        {
            var hash = ComputeSha256Hash(token);
            var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.TokenHash == hash && ut.IsActive);

            if (userToken != null)
            {
                userToken.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevokeTokensByUserIdAsync(int userId)
        {
            var tokens = await _context.UserTokens
                                       .Where(ut => ut.IdUser == userId && ut.IsActive)
                                       .ToListAsync();

            foreach (var token in tokens)
            {
                token.RevokedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<UserToken?> ValidateRefreshTokenAsync(string token)
        {
            var hash = ComputeSha256Hash(token);

            return await _context.UserTokens
                .FirstOrDefaultAsync(ut => ut.TokenHash == hash && ut.IsActive);
        }

        #region Helpers
        private string GenerateRandomToken(int size = 64)
        {
            var randomBytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        #endregion
    }
}
