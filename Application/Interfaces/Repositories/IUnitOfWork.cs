namespace Events.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ILocationRepository LocationRepository { get; }
        public IEventCategoryRepository EventCategoryRepository { get; }
        public IEventRepository EventRepository { get; }
        public IUserRepository UserRepository { get; }
        public IParticipantRepository ParticipantRepository { get; }

        public Task SaveAsync(CancellationToken cancellationToken);
    }
}
