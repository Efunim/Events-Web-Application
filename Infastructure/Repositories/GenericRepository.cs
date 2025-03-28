using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<T>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken) =>
            await _context.Set<T>()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

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

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
        }
    }
}
