namespace Events.Application.Interfaces.UseCases
{
    public interface IDeleteEventUseCase
    {
        public Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
