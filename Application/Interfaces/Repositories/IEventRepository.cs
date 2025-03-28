using Events.Application.Specifications;
using Events.Application.Specifications.EventSpecifications;
using Events.Domain.Entities;

namespace Events.Application.Interfaces.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        public IEnumerable<Event> GetByCriteria(Specification<Event>? specification, int pageIndex, int pageSize);
        public Task<IEnumerable<Event>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
