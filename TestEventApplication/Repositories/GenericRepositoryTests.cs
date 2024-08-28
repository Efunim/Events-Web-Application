using Events.Domain.Repositories;
using Events.Domain.Exceptions;
using Events.Infastructure.Data;
using Events.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TestEventApplication.Repositories
{
    public class GenericRepositoryTests
    {
        private readonly ApplicationContext context;
        public GenericRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );
            context = new ApplicationContext(dbOptions.Options);
        }

        [Fact]
        public async Task TestInsertEntity()
        {
            var eventCategoryRep = new EventCategoryRepository(context);

            int id = await eventCategoryRep.InsertAsync(
                new Events.Domain.Entities.EventCategory
                {
                    Name = "TEST CATEGORY"
                },
                CancellationToken.None
            );

            Assert.True(id > 0);
        }

        [Fact]
        public async Task TestGetEntity()
        {
            var eventCategoryRep = new EventCategoryRepository(context);

            int id = await eventCategoryRep.InsertAsync(
                    new Events.Domain.Entities.EventCategory
                    {
                        Name = "TEST CATEGORY"
                    },
                    CancellationToken.None
                );
            var category = await eventCategoryRep.GetByIdAsync(id, CancellationToken.None);

            Assert.NotNull(category);
        }

        [Fact]
        public async Task TestGetEntities()
        {
            var locationRep = new LocationRepository(context);

            await locationRep.InsertAsync(
                    new Events.Domain.Entities.Location
                    {
                        StreetId = 1,
                        House = "054"
                    },
                    CancellationToken.None
                );

            await locationRep.InsertAsync(
                    new Events.Domain.Entities.Location
                    {
                        StreetId = 3,
                        House = "055"
                    },
                    CancellationToken.None
                );

            await locationRep.InsertAsync(
                    new Events.Domain.Entities.Location
                    {
                        StreetId = 2,
                        House = "056"
                    },
                    CancellationToken.None
                );
            await context.SaveChangesAsync(CancellationToken.None);

            var locations = await locationRep.GetPageAsync(0, 2, CancellationToken.None);

            Assert.True(locations.Count == 2);
        }

        [Fact]
        public async Task TestGetEntitiy_ReturnsNull()
        {
            var locationRep = new LocationRepository(context);

            int id = await locationRep.InsertAsync(
                    new Events.Domain.Entities.Location
                    {
                        StreetId = 1,
                        House = "locationtodelete"
                    },
                    CancellationToken.None
                );
            await context.SaveChangesAsync(CancellationToken.None);
            var location = await locationRep.GetByIdAsync(id, CancellationToken.None);

            locationRep.Delete(location);
            await context.SaveChangesAsync(CancellationToken.None);

            location = await locationRep.GetByIdAsync(id, CancellationToken.None);

            Assert.Null(location);
        }

        [Fact]
        public async Task TestUpdateEntity()
        {
            var participantRep = new ParticipantRepository(context);

            int id = await participantRep.InsertAsync(
                    new Events.Domain.Entities.Participant
                    {
                        EventId = 1,
                        UserId = 1,
                        RegistrationDate = DateTime.Now
                    },
                    CancellationToken.None
                );
            await context.SaveChangesAsync(CancellationToken.None);
            var participant = await participantRep.GetByIdAsync(id, CancellationToken.None);
            participant.UserId = 2;

            participantRep.Update(participant);
            await context.SaveChangesAsync(CancellationToken.None);

            Assert.True(participant.UserId == 2);
        }
    }
}
