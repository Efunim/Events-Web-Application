using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;

namespace Events.Infastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationContext context) : base(context) { }
    }
}
