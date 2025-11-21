using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Infraestructure.Security.Authentication;
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
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly IUserTokenRepository _userTokenRepository;

        public AuthService(IUserRepository userRepository, JwtTokenGenerator tokenGenerator, IUserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailWithRoleAsync(email);
            if (user == null || !PasswordHasher.Verify(password, user.Password))
            {
                return new AuthResultDto { Success = false, ErrorMessage = "Invalid credentials" };
            }

            var roles = user!.UserRoles?.Select(ur => ur.RoleObj.RoleName).ToImmutableList()
                    ?? ImmutableList<string>.Empty;

            var accessToken = _tokenGenerator.GenerateToken(user!.IdUser, user.Email, roles);
            var refreshToken = await _userTokenRepository.CreateRefreshTokenAsync(user.IdUser);

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
            await _userTokenRepository.RevokeTokensByUserIdAsync(userId);
        }

        public string GenerateToken(User user)
        {
            var roles = user.UserRoles?.Select(ur => ur.RoleObj.RoleName).ToImmutableList()
                    ?? ImmutableList<string>.Empty;

            return _tokenGenerator.GenerateToken(user.IdUser, user.Email, roles);
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var token = await _userTokenRepository.ValidateRefreshTokenAsync(refreshToken);
            if (token == null)
                throw new Exception("Refresh token inválido");

            var user = await _userRepository.GetByIdAsync(token.IdUser);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            var roles = user.UserRoles?.Select(ur => ur.RoleObj.RoleName).ToImmutableList()
                    ?? ImmutableList<string>.Empty;


            return _tokenGenerator.GenerateToken(user.IdUser, user.Email, roles);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null && PasswordHasher.Verify(password, user.Password);
        }

        public Task<(int, string, IEnumerable<string>)?> GetUserFromTokenAsync(string token)
        {
            return Task.FromResult(_tokenGenerator.ReadToken(token));
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            await _userTokenRepository.RevokeTokenAsync(refreshToken);
        }
    }
}
