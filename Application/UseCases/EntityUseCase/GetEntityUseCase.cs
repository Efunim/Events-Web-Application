using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Services;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class GetEntityUseCase<TEntity, TResponseDto> : EntityUseCase<TEntity>
        where TEntity : BaseEntity
        where TResponseDto : class
    {
        public GetEntityUseCase(IGenericRepository<TEntity> repository, IMapper mapper) 
            : base(repository, mapper) { }

        public async Task<TResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await ServiceHelper.GetEntityAsync
                (_repository.GetByIdAsync, id, cancellationToken);

            return _mapper.Map<TEntity, TResponseDto>(entity);
        }
    }
}
