using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Services;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using System.Threading;

namespace Events.Application.UseCases
{
    public class UpdateEventCategoryUseCase : UpdateEntityUseCase<EventCategory, EventCategoryRequestDto>, IUpdateEventCategoryUseCase
    {
        public UpdateEventCategoryUseCase(IEventCategoryRepository repository, IMapper mapper) : base(repository, mapper) 
        {
            _validationUseCase = new ValidateEventCategoryUseCase(repository);
        }
    }
}
