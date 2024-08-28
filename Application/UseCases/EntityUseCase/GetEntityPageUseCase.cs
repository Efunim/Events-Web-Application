using AutoMapper;
using Events.Application.DTO.ResponseDTO;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class GetEntityPageUseCase<TEntity, TResponseDto> : EntityUseCase<TEntity>
        where TEntity : BaseEntity
    {
        public GetEntityPageUseCase(IGenericRepository<TEntity> repository, IMapper mapper) : base(repository, mapper) { }  

        public async Task<IEnumerable<TResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            if (pageIndex < 0 || pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Page size or index is invalid");
            }
            var entitiesPage = await _repository.GetPageAsync(pageIndex, pageSize, cancellationToken);

            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TResponseDto>>(entitiesPage);
        }
    }
}
