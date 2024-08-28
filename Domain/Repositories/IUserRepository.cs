using Events.Domain.Entities;

namespace Events.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByEmailAndPasswordHashAsync(string email, string passwordHash, CancellationToken cancellationToken);
        public Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
