using EbookBackend.Application.Dto;
using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(string email, string password);
        Task LogoutAsync(int userId);
        string GenerateToken(User user);
        Task<string> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task<(int, string, IEnumerable<string>)?> GetUserFromTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
