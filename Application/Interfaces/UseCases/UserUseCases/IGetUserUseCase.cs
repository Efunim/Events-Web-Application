using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetUserUseCase
    {
        public Task<UserResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
        public Task<User?> ExecuteAsync(string email, string passwordHash, CancellationToken cancellationToken);
        public Task<User?> ExecuteAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
