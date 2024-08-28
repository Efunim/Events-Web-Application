using Events.Application.Interfaces.UseCases;
using Events.Application.Services;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class StoreRefreshTokenUseCase(IUserRepository repository) : IStoreRefreshTokenUseCase
    {
        public async Task ExecuteAsync(int id, string refreshToken, CancellationToken cancellationToken)
        {
            var user = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            repository.Update(user);
        }
    }
}
