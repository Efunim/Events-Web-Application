using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;

namespace Events.Infastructure.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationContext context) : base(context) { }
    }
}
