using Events.Domain.Entities;

namespace Events.Application.Interfaces.Repositories
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        public Task SendEmailAsync(int userId);
    }
}
