using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Filters;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Validators;
using Events.Domain.Entities;

namespace Events.Application.Services
{
    public class EventService(IUnitOfWork uow, IMapper mapper) : IEventService
    {
        private readonly IEventRepository repository = uow.EventRepository;

        public async Task<EventResponseDto> GetEventAsync(int id, CancellationToken cancellationToken)
        {
            var @event = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            return mapper.Map<Event, EventResponseDto>(@event);
        }

        public async Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(CancellationToken cancellationToken)
        {
            var events = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<Event>, IEnumerable<EventResponseDto>>(events);
        }

        public async Task<int> InsertEventAsync(EventRequestDto eventDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(eventDto, cancellationToken);
            var @event = mapper.Map<EventRequestDto, Event>(eventDto);

            var id = await repository.InsertAsync(@event, cancellationToken);
            await uow.SaveAsync(cancellationToken);

            return id;
        }

        public async Task UpdateEventAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(eventDto, cancellationToken);

            var @event = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            mapper.Map(eventDto, @event);
            repository.Update(@event);

            await uow.SaveAsync(cancellationToken);
        }

        public async Task DeleteEventAsync(int id, CancellationToken cancellationToken)
        {
            var @event = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            repository.Delete(@event);

            await uow.SaveAsync(cancellationToken);
        }

        private async Task ValidateCategory(EventRequestDto @event, CancellationToken cancellationToken)
        {
            EventRequestDtoValidator validator = new EventRequestDtoValidator();
            var results = await validator.ValidateAsync(@event);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }

        public Task<IEnumerable<EventResponseDto>> GetEventsByCriteriaAsync(EventFilter eventFilter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
