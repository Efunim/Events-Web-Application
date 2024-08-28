using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;


namespace Events.Infastructure.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationContext context) : base(context) { }
    }
}
