using Events.Application.Filters;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        public Task<IEnumerable<Event>> GetByCriteriaAsync(EventFilter filter);
        public Task<IEnumerable<Event>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
