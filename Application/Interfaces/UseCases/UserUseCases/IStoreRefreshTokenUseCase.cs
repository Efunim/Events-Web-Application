namespace Events.Application.Interfaces.UseCases
{
    public interface IStoreRefreshTokenUseCase
    {
        public Task ExecuteAsync(int id, string refreshToken, CancellationToken cancellationToken);
    }
}
