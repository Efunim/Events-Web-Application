using Events.Domain.Entities;

namespace Events.Domain.Repositories
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        public Task SendEmailAsync(int userId);
    }
}
