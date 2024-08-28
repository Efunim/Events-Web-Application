using AutoMapper;
using Events.Application.DTO.BaseDTO;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private readonly IAddLocationUseCase _addUseCase;
        private readonly IDeleteLocationUseCase _deleteUseCase;
        private readonly IGetLocationUseCase _getUseCase;
        private readonly IGetLocationsPageUseCase _getPageUseCase;
        private readonly IUpdateLocationUseCase _updateUseCase;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LocationController(
        IAddLocationUseCase addUseCase,
        IDeleteLocationUseCase deleteUseCase,
        IGetLocationUseCase getUseCase,
        IGetLocationsPageUseCase getPageUseCase,
        IUpdateLocationUseCase updateUseCase,
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

        [HttpGet(Name = "GetLocation")]
        public async Task<LocationResponseDto> GetLocationAsync(int id, CancellationToken cancellationToken) =>
            await _getUseCase.ExecuteAsync(id, cancellationToken);

        [HttpGet(Name = "GetLocationsPage")]
        public async Task<IEnumerable<LocationResponseDto>> GetLocationsPage(CancellationToken cancellationToken,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            return await _getPageUseCase.ExecuteAsync(pageIndex, pageSize, cancellationToken);
        }

        [HttpPost(Name = "InsertLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertLocationAsync(LocationRequestDto locationDto, CancellationToken cancellationToken)
        {
            int id = await _addUseCase.ExecuteAsync(locationDto, cancellationToken);
            await _uow.SaveAsync(cancellationToken);

            return id;
        }

        [HttpPut(Name = "UpdateLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateLocationAsync(int id, LocationRequestDto locationDto, CancellationToken cancellationToken)
        {
            await _updateUseCase.ExecuteAsync(id, locationDto, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        [HttpDelete(Name = "DeleteLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteLocationAsync(int id, CancellationToken cancellationToken)
        {
            await _deleteUseCase.ExecuteAsync(id, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }
    }
}

