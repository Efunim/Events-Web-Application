using AutoMapper;
using Events.Domain.Entities;
using Events.Domain.Repositories;


namespace Events.Application.UseCases.EntityUseCase
{
    public abstract class EntityUseCase<TEntity> where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public EntityUseCase(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public EntityUseCase(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }
    }
}
