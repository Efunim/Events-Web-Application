using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetEventCategoryUseCase
    {
        public Task<EventCategoryResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
