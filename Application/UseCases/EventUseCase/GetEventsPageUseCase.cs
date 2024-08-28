using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.Specifications;
using Events.Domain.Specifications.EventSpecifications;
using System.Collections.Generic;

namespace Events.Application.UseCases
{
    public class GetEventsPageUseCase : GetEntityPageUseCase<Event, EventResponseDto>, IGetEventsPageUseCase
    {
        public GetEventsPageUseCase(IEventRepository repository, IMapper mapper) : base(repository, mapper) { }
        public async Task<IEnumerable<EventResponseDto>> ExecuteAsync(ISpecification<Event> filter, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var page = await ((IEventRepository)_repository).GetByCriteriaAsync(filter, pageIndex, pageSize, cancellationToken);

            return _mapper.Map<IEnumerable<Event>, IEnumerable<EventResponseDto>>(page);
        }
    }
}
