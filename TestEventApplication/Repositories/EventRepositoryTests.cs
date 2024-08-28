using Events.Domain.Specifications;
using Events.Domain.Specifications.EventSpecifications;
using Events.Domain.Entities;
using Events.Infastructure.Data;
using Events.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace TestEventApplication.Repositories
{
    public class EventRepositoryTests
    {
        private readonly ApplicationContext context;
        public EventRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );
            context = new ApplicationContext(dbOptions.Options);
        }

        [Fact]
        public async Task GetPageAsync()
        {
            // Arrange
            var repository = new EventRepository(context);

            var events = new List<Event>()
            {
                new Event
                {
                    Id = 1,
                    Name = "Event1",
                    Description = "Description1",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
                new Event
                {
                    Id = 2,
                    Name = "Event2",
                    Description = "Description",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
                new Event
                {
                    Id = 3,
                    Name = "Event3",
                    Description = "Description",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
            };

            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();

            var pageIndex = 0;
            var pageSize = 2;

            // Act
            var result = await repository.GetPageAsync(pageIndex, pageSize, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(pageSize == result.Count());
            Assert.Contains(result, e => e.Name == events[0].Name);
            Assert.Contains(result, e => e.Name == events[1].Name);
        }

        [Fact]
        public async Task GetByCriteria_ReturnsCorrectEvents()
        {
            // Arrange
            var repository = new EventRepository(context);

            var events = new List<Event>()
            {
                new Event
                {
                    Id = 1,
                    Name = "Event1",
                    Description = "Description1",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
                new Event
                {
                    Id = 2,
                    Name = "Event2",
                    Description = "Description",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
                new Event
                {
                    Id = 3,
                    Name = "Event3",
                    Description = "Description",
                    CategoryId = 1,
                    LocationId = 1,
                    EventTime = DateTime.UtcNow.AddDays(7),
                    MaxParticipants = 20,
                    ImagePath = "images\\G-x--WwY-RA.jpg"
                },
            };

            context.Events.AddRange(events);
            context.SaveChanges();

            var specification = new EventNameSpecification(name: "event");
            var pageIndex = 0;
            var pageSize = 3;

            // Act
            var result = await repository.GetByCriteriaAsync(specification, pageIndex, pageSize, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(events.Count, result.Count());
            Assert.Contains(result, e => e.Name == events[0].Name);
            Assert.Contains(result, e => e.Name == events[1].Name);
            Assert.Contains(result, e => e.Name == events[2].Name);
        }
    }
}
