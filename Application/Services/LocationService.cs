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
    public class LocationService(IUnitOfWork uow, IMapper mapper) : ILocationService
    {
        private readonly ILocationRepository repository = uow.LocationRepository;

        public async Task<LocationResponseDto> GetLocationAsync(int id, CancellationToken cancellationToken)
        {
            var location = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            return mapper.Map<Location, LocationResponseDto>(location);
        }

        public async Task<IEnumerable<LocationResponseDto>> GetAllLocationsAsync(CancellationToken cancellationToken)
        {
            var locations = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<Location>, IEnumerable<LocationResponseDto>>(locations);
        }

        public async Task<int> InsertLocationAsync(LocationRequestDto locationDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(locationDto, cancellationToken);
            var location = mapper.Map<LocationRequestDto, Location>(locationDto);

            var id = await repository.InsertAsync(location, cancellationToken);
            await uow.SaveAsync(cancellationToken);

            return id;
        }

        public async Task UpdateLocationAsync(int id, LocationRequestDto locationDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(locationDto, cancellationToken);

            var location = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            mapper.Map(locationDto, location);
            repository.Update(location);

            await uow.SaveAsync(cancellationToken);
        }

        public async Task DeleteLocationAsync(int id, CancellationToken cancellationToken)
        {
            var location = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            repository.Delete(location);

            await uow.SaveAsync(cancellationToken);
        }

        private async Task ValidateCategory(LocationRequestDto location, CancellationToken cancellationToken)
        {
            LocationRequestDtoValidator validator = new LocationRequestDtoValidator();
            var results = await validator.ValidateAsync(location);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
