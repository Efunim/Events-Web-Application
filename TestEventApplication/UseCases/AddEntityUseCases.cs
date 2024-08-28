using AutoMapper;
using Events.Application.DTO.MappingProfiles;
using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Moq;

namespace TestEventApplication.UseCases
{
    public class AddEntityUseCases
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly IMapper _mapper;

        public AddEntityUseCases()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _locationRepositoryMock = new Mock<ILocationRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventMappingProfile>();
                cfg.AddProfile<LocationMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task AddEvent()
        {
            // Arrange
            var addUseCase = new AddEventUseCase(_eventRepositoryMock.Object, _mapper);
            var eventRequest = ObjectsGen.GetEventRequest();

            _eventRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await addUseCase.ExecuteAsync(eventRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task AddLocation()
        {
            // Arrange
            var addUseCase = new AddLocationUseCase(_locationRepositoryMock.Object, _mapper);
            var locationRequest = ObjectsGen.GetLocationRequest();

            _locationRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await addUseCase.ExecuteAsync(locationRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task AddEvent_ReturnsException()
        {
            // Arrange
            var addUseCase = new AddEventUseCase(_eventRepositoryMock.Object, _mapper);
            var eventRequest = ObjectsGen.GetEventRequest();
            eventRequest.Name = "";

            _eventRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            await Assert.ThrowsAsync<ValidationException>(() =>
                addUseCase.ExecuteAsync(eventRequest, CancellationToken.None));
        }
    }
}
