using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class GetLocationsPageUseCase : GetEntityPageUseCase<Location, LocationResponseDto>, IGetLocationsPageUseCase
    {
        public GetLocationsPageUseCase(ILocationRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
