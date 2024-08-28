using Events.Domain.Repositories;
using Events.Infastructure.Data;

namespace Events.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public ILocationRepository LocationRepository { get; }
        public IEventCategoryRepository EventCategoryRepository { get; }
        public IEventRepository EventRepository { get; }
        public IUserRepository UserRepository { get; }
        public IParticipantRepository ParticipantRepository { get; }

        private bool _disposed = false;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            LocationRepository = new LocationRepository(_context);
            EventCategoryRepository = new EventCategoryRepository(_context);
            EventRepository = new EventRepository(_context);
            UserRepository = new UserRepository(_context);
            ParticipantRepository = new ParticipantRepository(_context);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }

            _disposed = true;
        }
    }
}
