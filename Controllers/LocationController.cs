using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController(ILocationService locationService) : ControllerBase
    {
        [HttpGet(Name = "GetLocation")]
        public async Task<LocationResponseDto> GetLocationAsync(int id, CancellationToken cancellationToken) =>
            await locationService.GetLocationAsync(id, cancellationToken);

        [HttpGet(Name = "GetAllLocations")]
        public async Task<IEnumerable<LocationResponseDto>> GetAllLocationsAsync(CancellationToken cancellationToken) =>
            await locationService.GetAllLocationsAsync(cancellationToken);

        [HttpPost(Name = "InsertLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertLocationAsync(LocationRequestDto LocationDto, CancellationToken cancellationToken) =>
            await locationService.InsertLocationAsync(LocationDto, cancellationToken);

        [HttpPut(Name = "UpdateLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateLocationAsync(int id, LocationRequestDto LocationDto, CancellationToken cancellationToken) =>
            await locationService.UpdateLocationAsync(id, LocationDto, cancellationToken);

        [HttpDelete(Name = "DeleteLocation")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteLocationAsync(int id, CancellationToken cancellationToken) =>
            await locationService.DeleteLocationAsync(id, cancellationToken);
    }
}

