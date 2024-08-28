using Events.Application.Services;
using Events.Domain.Entities;
using Events.Domain.Repositories;

namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class DeleteEntityUseCase<TEntity> : EntityUseCase<TEntity>
        where TEntity : BaseEntity
    {
        public DeleteEntityUseCase(IGenericRepository<TEntity> repository) : base (repository) { }

        public async Task ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await ServiceHelper.GetEntityAsync(_repository.GetByIdAsync, id, cancellationToken);
            _repository.Delete(entity);
        }
    }
}
