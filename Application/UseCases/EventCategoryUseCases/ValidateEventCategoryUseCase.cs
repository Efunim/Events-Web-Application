using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.Validators;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class ValidateEventCategoryUseCase(IEventCategoryRepository repository) : IValidationUseCase<EventCategoryRequestDto>
    {
        public async Task ExecuteAsync(EventCategoryRequestDto eventCategory, CancellationToken cancellationToken)
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
