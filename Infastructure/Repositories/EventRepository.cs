using Events.Application.Interfaces.Repositories;
using Events.Application.Specifications;
using Events.Application.Specifications.EventSpecifications;
using Events.Domain.Entities;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<Event> GetByCriteria(Specification<Event> specification, int pageIndex, int pageSize)
        {
            return SpecificationQueryBuilder
                .GetQuery(_context.Events, specification, pageIndex, pageSize);
        }

        public async Task<IEnumerable<Event>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken) =>
            await _context.Set<Event>()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

    }
}
