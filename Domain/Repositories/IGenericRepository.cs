using Events.Domain.Entities;
using System.Linq.Expressions;

namespace Events.Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<T>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<int> InsertAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
