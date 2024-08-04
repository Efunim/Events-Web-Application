using Events.Application.Filters;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        public Task<List<Event>> GetByCriteriaAsync(EventFilter filter);
    }
}
