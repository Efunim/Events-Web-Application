using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.Specifications.EventSpecifications;

namespace Events.Application.UseCases
{ 
    public class GetPageEventCategoriesUseCase : GetEntityPageUseCase<EventCategory, EventCategoryResponseDto>, IGetPageEventCategoriesUseCase
    {
        public GetPageEventCategoriesUseCase(IEventCategoryRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
