using AutoMapper;
using Events.Application.DTO;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Events.Application.Specifications;
using Events.Application.Specifications.EventSpecifications;
using Events.Domain.Entities;
using Moq;

namespace TestEventApplication.Services
{
    public class EventServiceTests
    {

        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper mapper;

        public EventServiceTests()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingProfile>();
            });
            mapper = config.CreateMapper();

            _unitOfWorkMock.Setup(uow => uow.EventRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetEventAsync_ReturnsException()
        {
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                service.GetEventAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetEventAsync()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            var eventRequest = GetEventRequest();
            var @eventToGet = GetEventObject();

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(@eventToGet);

            // Act
            int id = await service.InsertEventAsync(eventRequest, CancellationToken.None);
            var @event = await service.GetEventAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(@event);
            Assert.Equal(eventRequest.Name, @event.Name);
        }

        [Fact]
        public async Task GetEventPage()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            var eventCategories = GetEventsList();
            int count = 2;

            _repositoryMock.Setup(repo => repo.GetPageAsync(It.IsAny<int>(), It.IsAny<int>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync(eventCategories.Take(count));

            // Act
            var result = await service.GetPageAsync(0, count, CancellationToken.None);

            // Assert
            Assert.True(result.Count() == count);
        }

        [Fact]
        public async Task GetEventByCityId()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            var events = GetEventsList();
            int pageIndex = 0;
            int pageSize = 2;

            _repositoryMock.Setup(repo => repo.GetByCriteria(It.IsAny<Specification<Event>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(events.AsQueryable().Take(pageSize));

            var specification = new EventSpecification(cityId: 1);

            // Act
            var result = service.GetEventsByCriteria(specification, pageIndex, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == pageSize);
        }

        [Fact]
        public async Task GetAllEventsAsync()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            var eventCategories = GetEventsList();
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(eventCategories);

            // Act
            var categories = await service.GetAllEventsAsync(CancellationToken.None);

            // Assert
            Assert.True(categories.Count() == eventCategories.Count);
        }

        [Fact]
        public async Task InsertEvent()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);
            var eventRequest = GetEventRequest();

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await service.InsertEventAsync(eventRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task UpdateEvent()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);

            var old = GetEventObject();
            var updatedDto = GetEventRequest();
            updatedDto.Name = "Updated";

            _repositoryMock.Setup(repo => repo.GetByIdAsync(old.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(old);

            // Act
            await service.UpdateEventAsync(old.Id, updatedDto, CancellationToken.None);
            var @event = await service.GetEventAsync(old.Id, CancellationToken.None);

            // Assert
            Assert.True(@event.Id == old.Id);
            Assert.True(@event.Name == updatedDto.Name);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            var service = new EventService(_unitOfWorkMock.Object, mapper);

            var existing = GetEventObject();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existing);

            // Act
            await service.DeleteEventAsync(1, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.Is<Event>(e => e.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        private Event GetEventObject()
        {
            return new Event
            {
                Id = 1,
                Name = "Event",
                Description = "Description",
                CategoryId = 1,
                LocationId = 1,
                EventTime = DateTime.UtcNow.AddDays(7),
                MaxParticipants = 20,
                ImagePath = "images\\G-x--WwY-RA.jpg"
            };
        }

        private EventRequestDto GetEventRequest()
        {
            return new EventRequestDto
            {
                LocationId = 1,
                CategoryId = 1,
                Name = "Event",
                Description = "Description",
                EventTime = DateTime.UtcNow.AddDays(7),
                MaxParticipants = 20,
            };
        }

        private List<Event> GetEventsList()
        {
            return new List<Event>
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
        }
    }
}
