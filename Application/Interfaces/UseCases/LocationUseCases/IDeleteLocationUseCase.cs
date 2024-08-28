namespace Events.Application.Interfaces.UseCases
{
    public interface IDeleteLocationUseCase
    {
        public Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
