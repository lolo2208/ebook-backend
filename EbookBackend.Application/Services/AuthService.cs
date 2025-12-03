using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Domain.Security;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            ITokenGenerator tokenGenerator, 
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.GetByEmailWithRoleAsync(email);

            if (user == null)
                return new AuthResultDto { Success = false, ErrorMessage = "Invalid credentials" };

            var verification = _passwordHasher.Verify(password, user.Password);
            if (verification == false)
                return new AuthResultDto { Success = false, ErrorMessage = "Invalid credentials" };

            var roles = user.UserRoles?.Select(x => x.RoleObj.RoleName).ToImmutableList()
                       ?? ImmutableList<string>.Empty;

            var accessToken = _tokenGenerator.GenerateToken(user.IdUser, user.Email, roles);
            var refreshToken = await _unitOfWork.UserTokens.CreateRefreshTokenAsync(user.IdUser);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResultDto
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }


        public async Task LogoutAsync(int userId)
        {
            await _unitOfWork.UserTokens.RevokeTokensByUserIdAsync(userId);
            await _unitOfWork.SaveChangesAsync();
        }

        public string GenerateToken(User user)
        {
            var roles = user.UserRoles?.Select(ur => ur.RoleObj.RoleName).ToImmutableList()
                    ?? ImmutableList<string>.Empty;

            return _tokenGenerator.GenerateToken(user.IdUser, user.Email, roles);
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.UserTokens.ValidateRefreshTokenAsync(refreshToken);
            if (token == null)
                throw new Exception("Refresh token inválido");

            var user = await _unitOfWork.Users.GetByIdAsync(token.IdUser);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            var roles = user.UserRoles?.Select(ur => ur.RoleObj.RoleName).ToImmutableList()
                    ?? ImmutableList<string>.Empty;

            await _unitOfWork.SaveChangesAsync();
            return _tokenGenerator.GenerateToken(user.IdUser, user.Email, roles);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);
            if (user == null) return false;

            var result = _passwordHasher.Verify(password, user.Password);
            return result;
        }

        public Task<(int, string, IEnumerable<string>)?> GetUserFromTokenAsync(string token)
        {
            return Task.FromResult(_tokenGenerator.ReadToken(token));
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            await _unitOfWork.UserTokens.RevokeTokenAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
