using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Set<T>().FindAsync([id], cancellationToken);

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
            => await _context.Set<T>().ToListAsync(cancellationToken);

        public virtual async Task<int> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            var entityEntry = await _context.Set<T>().AddAsync(entity, cancellationToken);
            return entityEntry.Entity.Id;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
