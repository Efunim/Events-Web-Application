using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Specifications.EventSpecifications;
using Events.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Repositories;
using Events.Domain.Specifications;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IAddEventUseCase _addUseCase;
        private readonly IDeleteEventUseCase _deleteUseCase;
        private readonly IGetEventUseCase _getUseCase;
        private readonly IGetEventsPageUseCase _getPageUseCase;
        private readonly IUpdateEventUseCase _updateUseCase;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EventController(
        IAddEventUseCase addUseCase,
        IDeleteEventUseCase deleteUseCase,
        IGetEventUseCase getUseCase,
        IGetEventsPageUseCase getPageUseCase,
        IUpdateEventUseCase updateUseCase,
        IUnitOfWork uow,
        IMapper mapper)
        {
            _addUseCase = addUseCase;
            _deleteUseCase = deleteUseCase;
            _getUseCase = getUseCase;
            _getPageUseCase = getPageUseCase;
            _updateUseCase = updateUseCase;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetEvent")]
        public async Task<EventResponseDto> GetEventAsync(int id, CancellationToken cancellationToken) =>
            await _getUseCase.ExecuteAsync(id, cancellationToken);

        [HttpGet(Name = "GetEventsPage")]
        public async Task<IEnumerable<EventResponseDto>> GetEventsPageAsync(CancellationToken cancellationToken,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            return await _getPageUseCase.ExecuteAsync(pageIndex, pageSize, cancellationToken);
        }

        [HttpGet(Name = "GetEventsByCriteria")]
        public async Task<IEnumerable<EventResponseDto>> GetEventsByCriteriaAsync(
            CancellationToken cancellationToken,
            [FromQuery] IEnumerable<int>? categoryIs = null,
            [FromQuery] string name = "",
            [FromQuery] int? cityId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            var specifications = ComposeSpecifications(categoryIs, name, cityId, startDate, endDate);

            return await _getPageUseCase.ExecuteAsync(specifications, pageIndex, pageSize, cancellationToken);
        }

        [HttpPost(Name = "InsertEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertEventAsync(EventRequestDto eventDto, CancellationToken cancellationToken)
        {
            int id = await _addUseCase.ExecuteAsync(eventDto, cancellationToken);
            await _uow.SaveAsync(cancellationToken);

            return id;
        }

        [HttpPut(Name = "UpdateEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateEventAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken)
        {
            await _updateUseCase.ExecuteAsync(id, eventDto, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        [HttpDelete(Name = "DeleteEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteEventAsync(int id, CancellationToken cancellationToken)
        {
            await _deleteUseCase.ExecuteAsync(id, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        private CompositeSpecification<Event> ComposeSpecifications(
            IEnumerable<int>? categoryIds = null,
            string name = "",
            int? cityId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var categorySpec = new EventCategorySpecification(categoryIds);
            var nameSpec = new EventNameSpecification(name);
            var citySpec = new EventCitySpecification(cityId);
            var dateSpec = new EventDateSpecification(startDate, endDate);

            return new CompositeSpecification<Event>(categorySpec, nameSpec, citySpec, dateSpec);
        }
    }
}
