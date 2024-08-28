using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases;
using Events.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventCategoryController
        : ControllerBase
    {
        private readonly IAddEventCategoryUseCase _addUseCase;
        private readonly IDeleteEventCategoryUseCase _deleteUseCase;
        private readonly IGetEventCategoryUseCase _getUseCase;
        private readonly IGetPageEventCategoriesUseCase _getPageUseCase;
        private readonly IUpdateEventCategoryUseCase _updateUseCase;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EventCategoryController(
        IAddEventCategoryUseCase addUseCase,
        IDeleteEventCategoryUseCase deleteUseCase,
        IGetEventCategoryUseCase getUseCase,
        IGetPageEventCategoriesUseCase getPageUseCase,
        IUpdateEventCategoryUseCase updateUseCase,
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

        [HttpGet(Name = "GetEventCategory")]
        public async Task<EventCategoryResponseDto> GetEventCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return await _getUseCase.ExecuteAsync(id, cancellationToken);
        }

        [HttpGet(Name = "GetEventCategoriesPage")]
        public async Task<IEnumerable<EventCategoryResponseDto>> GetEventCategoriesPageAsync(CancellationToken cancellationToken,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            return await _getPageUseCase.ExecuteAsync(pageIndex, pageSize, cancellationToken);
        }

        [HttpPost(Name = "InsertEventCategory")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertEventCategoryAsync(EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken)
        {
            int id = await _addUseCase.ExecuteAsync(eventCategoryDto, cancellationToken);
            await _uow.SaveAsync(cancellationToken);

            return id;
        }

        [HttpPut(Name = "UpdateEventCategory")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateEventCategoryAsync(int id, EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken)
        {
            await _updateUseCase.ExecuteAsync(id, eventCategoryDto, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        [HttpDelete(Name = "DeleteEventCategory")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteEventCategoryAsync(int id, CancellationToken cancellationToken)
        {
            await _deleteUseCase.ExecuteAsync(id, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }
    }
}
