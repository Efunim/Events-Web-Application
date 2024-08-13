using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Filters;
using Events.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventController(IEventService eventService) : ControllerBase
    {
        [HttpGet(Name = "GetEvent")]
        public async Task<EventResponseDto> GetEventAsync(int id, CancellationToken cancellationToken) =>
            await eventService.GetEventAsync(id, cancellationToken);

        [HttpGet(Name = "GetEventPage")]
        public async Task<IEnumerable<EventResponseDto>> GetEventsPage(CancellationToken cancellationToken,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            return await eventService.GetPageAsync(pageIndex, pageSize, cancellationToken);
        }

        [HttpGet(Name = "GetEventsByCriteria")]
        public async Task<IEnumerable<EventResponseDto>> GetEventsByCriteriaAsync(EventFilter eventFilter, CancellationToken cancellationToken) =>
            await eventService.GetEventsByCriteriaAsync(eventFilter, cancellationToken);

        [HttpGet(Name = "GetAllEvents")]
        public async Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(CancellationToken cancellationToken) =>
            await eventService.GetAllEventsAsync(cancellationToken);

        [HttpPost(Name = "InsertEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertEventAsync(EventRequestDto eventDto, CancellationToken cancellationToken) =>
            await eventService.InsertEventAsync(eventDto, cancellationToken);

        [HttpPut(Name = "UpdateEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateEventAsync(int id, EventRequestDto eventDto, CancellationToken cancellationToken) =>
            await eventService.UpdateEventAsync(id, eventDto, cancellationToken);

        [HttpDelete(Name = "DeleteEvent")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteEventAsync(int id, CancellationToken cancellationToken) =>
            await eventService.DeleteEventAsync(id, cancellationToken);
    }
}
