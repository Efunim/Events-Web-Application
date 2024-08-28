using AutoMapper;
using Events.Application;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Events.Infastructure.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IGetUserUseCase _getUseCase;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AuthentificationService(IGetUserUseCase getUseCase, IUnitOfWork uow, IMapper mapper) 
        {
            _getUseCase = getUseCase;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse?> AuthenticateAsync(UserRequestDto userRequest, CancellationToken cancellationToken)
        {
            var passwordHash = HashPassword(userRequest.Password);
            var user = await _getUseCase.ExecuteAsync(userRequest.Email, passwordHash, cancellationToken);

            if (user == null) return null;

            var newRefreshToken = GenerateRefreshToken();
            var response = GenerateResponse(user, newRefreshToken);
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(2);

            _uow.UserRepository.Update(user);
            await _uow.SaveAsync(cancellationToken);

            return response;
        }

        public async Task<AuthenticateResponse?> ValidateRefreshTokenAsync(string? refreshToken, CancellationToken cancellationToken)
        {
            if (refreshToken == null) return null;

            var user = await _getUseCase.ExecuteAsync(refreshToken, cancellationToken);

            if (user == null) return null;

            return GenerateResponse(user, refreshToken);
        }

        private AuthenticateResponse GenerateResponse(User user, string refreshToken)
        {
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse
            {
                User = _mapper.Map<UserResponseDto>(user),
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(
            new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddHours(7),
                Issuer = AuthConfiguration.ISSUER,
                Audience = AuthConfiguration.AUDIENCE,
                SigningCredentials = new SigningCredentials(AuthConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}
