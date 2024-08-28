using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.Interfaces.UseCases;
using Events.Application.Services;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class UpdateEntityUseCase<TEntity, TRequestDto> : EntityUseCase<TEntity>
        where TEntity : BaseEntity
    {
        protected IValidationUseCase<TRequestDto> _validationUseCase;

        public UpdateEntityUseCase(IGenericRepository<TEntity> repository, IMapper mapper) : base(repository, mapper) { }

        public async Task ExecuteAsync(int id, TRequestDto entityDto, CancellationToken cancellationToken)
        {
            await _validationUseCase.ExecuteAsync(entityDto, cancellationToken);
            var category = await ServiceHelper.GetEntityAsync
                (_repository.GetByIdAsync, id, cancellationToken);

            _mapper.Map(entityDto, category);
            _repository.Update(category);
        }
    }
}
