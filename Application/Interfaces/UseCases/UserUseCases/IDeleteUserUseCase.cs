namespace Events.Application.Interfaces.UseCases
{
    public interface IDeleteUserUseCase
    {
        public Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
