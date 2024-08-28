using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Services;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class GetEventCategoryUseCase : GetEntityUseCase<EventCategory, EventCategoryResponseDto>, IGetEventCategoryUseCase
    {
        public GetEventCategoryUseCase(IEventCategoryRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
