namespace Events.Application.Interfaces.UseCases
{
    public interface IDeleteParticipantUseCase
    {
        public Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
