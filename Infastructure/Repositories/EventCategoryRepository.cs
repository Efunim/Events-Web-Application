using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;


namespace Events.Infastructure.Repositories
{
    public class EventCategoryRepository : GenericRepository<EventCategory>, IEventCategoryRepository
    {
        public EventCategoryRepository(ApplicationContext context) : base(context) { }
    }
}
