using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Interfaces
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
        Task<UserToken?> GetByTokenHashAsync(string tokenHash);
        Task<IEnumerable<UserToken>> GetByUserIdAsync(int userId);
        Task<string> CreateRefreshTokenAsync(int userId);
        Task RevokeTokenAsync(string token);
        Task RevokeTokensByUserIdAsync(int userId);
        Task<UserToken?> ValidateRefreshTokenAsync(string token);
    }
}
