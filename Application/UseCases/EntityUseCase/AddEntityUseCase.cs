using AutoMapper;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class AddEntityUseCase<TEntity, TRequestDto> : EntityUseCase<TEntity>
        where TEntity : BaseEntity
        where TRequestDto : class
    {
        protected IValidationUseCase<TRequestDto> _validationUseCase;

        public AddEntityUseCase(IGenericRepository<TEntity> repository, IMapper mapper) 
            : base(repository, mapper) 
        {}

        public virtual async Task<int> ExecuteAsync(TRequestDto dto, CancellationToken cancellationToken)
        {
            await _validationUseCase.ExecuteAsync(dto, cancellationToken);

            var entity = _mapper.Map<TRequestDto, TEntity>(dto);
            var id = await _repository.InsertAsync(entity, cancellationToken);
            return id;
        }
    }
}
