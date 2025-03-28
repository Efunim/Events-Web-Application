using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;

namespace Events.Infastructure.Repositories
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationContext context) : base(context) { }

        public Task SendEmailAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
