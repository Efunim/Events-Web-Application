using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class GetUserUseCase : GetEntityUseCase<User, UserResponseDto>, IGetUserUseCase
    {
        public GetUserUseCase(IUserRepository repository, IMapper mapper) : base(repository, mapper) { }

        public async Task<User?> ExecuteAsync(string email, string passwordHash, CancellationToken cancellationToken) =>
            await ((IUserRepository)_repository).GetByEmailAndPasswordHashAsync(email, passwordHash, cancellationToken);

        public async Task<User?> ExecuteAsync(string refreshToken, CancellationToken cancellationToken) =>
            await ((IUserRepository)_repository).GetByRefreshTokenAsync(refreshToken, cancellationToken);
    }
}
