using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Validators;
using Events.Domain.Entities;

namespace Events.Application.Services
{
    public class EventCategoryService(IUnitOfWork uow, IMapper mapper) : IEventCategoryService
    {
        private readonly IEventCategoryRepository repository = uow.EventCategoryRepository;

        public async Task<EventCategoryResponseDto> GetEventCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var category = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            return mapper.Map<EventCategory, EventCategoryResponseDto>(category);
        }

        public async Task<IEnumerable<EventCategoryResponseDto>> GetAllEventCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<EventCategory>, IEnumerable<EventCategoryResponseDto>>(categories);
        }

        public async Task<int> InsertEventCategoryAsync(EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(eventCategoryDto, cancellationToken);
            var eventCategory = mapper.Map<EventCategoryRequestDto, EventCategory>(eventCategoryDto);

            var id = await repository.InsertAsync(eventCategory, cancellationToken);
            await uow.SaveAsync(cancellationToken);

            return id;
        }

        public async Task UpdateEventCategoryAsync(int id, EventCategoryRequestDto eventCategoryDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(eventCategoryDto, cancellationToken);

            var category = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            category.Name = eventCategoryDto.Name;
            repository.Update(category);

            await uow.SaveAsync(cancellationToken);
        } 
        
        public async Task DeleteEventCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var category = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);
            repository.Delete(category);

            await uow.SaveAsync(cancellationToken);
        }

        private async Task ValidateCategory(EventCategoryRequestDto eventCategory, CancellationToken cancellationToken)
        {
            EventCategoryRequestDtoValidator validator = new EventCategoryRequestDtoValidator();
            var results = await validator.ValidateAsync(eventCategory);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }

            if (await CheckUniqueness(eventCategory.Name, cancellationToken))
            {
                throw new NonUniqueException("Category name already exists!");
            }
        }

        private async Task<bool> CheckUniqueness(string categoryName, CancellationToken cancellationToken)
        {
            return await repository.ExistsAsync(e => e.Name == categoryName, cancellationToken);
        }
    }
}
