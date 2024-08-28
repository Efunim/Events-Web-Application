using Events.Domain.Entities;
using Events.Domain.Specifications;

namespace Events.Domain.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        public Task<IEnumerable<Event>> GetByCriteriaAsync(ISpecification<Event> specification, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
