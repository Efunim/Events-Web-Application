using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases.EntityUseCase;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases
{
    public class GetLocationUseCase : GetEntityUseCase<Location, LocationResponseDto>, IGetLocationUseCase
    {
        public GetLocationUseCase(ILocationRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}
