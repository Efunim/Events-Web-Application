using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Infastructure.Data;


namespace Events.Infastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }
    }
}
