using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public async Task<User?> GetByEmailAndPasswordHashAsync(string email, string passwordHash, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash, cancellationToken);
        }
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow, cancellationToken);
        }
    }
}
