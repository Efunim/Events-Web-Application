namespace Events.Application.Interfaces.UseCases
{
    public interface IValidationUseCase<TRequestDto>
    {
        Task ExecuteAsync(TRequestDto dto, CancellationToken cancellationToken);
    }
}
