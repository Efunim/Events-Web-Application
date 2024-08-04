using Events.Application.Filters;
using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context) { }

        public async Task<List<Event>> GetByCriteriaAsync(EventFilter filter)
        {
            var query = _context.Events.AsQueryable();

            if (filter.StartDate.HasValue)
            {
                query = query.Where(e => e.EventTime >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(e => e.EventTime <= filter.EndDate.Value);
            }

            if (filter.Location != null)
            {
                // TODO: мне сейчас впадлу писать, потом надо нормальную фильтрацию сделать ;-;
            }

            if (filter.CategoryId != null)
            {
                query = query.Where(e => e.CategoryId == filter.CategoryId);
            }

            return await query.ToListAsync();
        }
    }
}
