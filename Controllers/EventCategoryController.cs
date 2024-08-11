using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventCategoryController(IEventCategoryService eventCategoryService) 
        : ControllerBase
    {
        [HttpGet(Name = "GetEventCategory")]
        public async Task<EventCategoryResponseDto> GetEventCategoryAsync(int id, CancellationToken cancellationToken) =>
            await eventCategoryService.GetEventCategoryAsync(id, cancellationToken);

        [HttpGet(Name = "GetAllEventCategories")]
        public async Task<IEnumerable<EventCategoryResponseDto>> GetAllEventCategoriesAsync(CancellationToken cancellationToken) =>
            await eventCategoryService.GetAllEventCategoriesAsync(cancellationToken);

        [HttpPost(Name = "InsertEventCategory")]
        public async Task<int> InsertEventCategoryAsync(EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken) =>
            await eventCategoryService.InsertEventCategoryAsync(eventCategoryDto, cancellationToken);

        [HttpPut(Name = "UpdateEventCategory")]
        public async Task UpdateEventCategoryAsync(int id, EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken) =>
            await eventCategoryService.UpdateEventCategoryAsync(id, eventCategoryDto, cancellationToken);

        [HttpDelete(Name = "DeleteEventCategory")]
        public async Task DeleteEventCategoryAsync(int id, CancellationToken cancellationToken) =>
            await eventCategoryService.DeleteEventCategoryAsync(id, cancellationToken);


    }
}
