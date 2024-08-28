using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{ 
    public interface IGetEventUseCase
    {
        public Task<EventResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
