using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;

namespace Events.Infastructure.Repositories
{
    public class StreetRepository : GenericRepository<Street>, IStreetRepository
    {
        public StreetRepository(ApplicationContext context) : base(context) { }
    }
}
