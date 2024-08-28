using Events.Application.DTO.RequestDTO;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.UseCases
{
    public interface IUpdateEventCategoryUseCase
    {
        public Task ExecuteAsync(int id, EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken);
    }
}
