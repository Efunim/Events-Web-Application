using Events.Application.Interfaces.Repositories;
using Events.Application.Specifications;
using Events.Application.Specifications.EventSpecifications;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.Specifications;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Event>> GetByCriteriaAsync(ISpecification<Event> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await specification.Apply(_context.Set<Event>())
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
