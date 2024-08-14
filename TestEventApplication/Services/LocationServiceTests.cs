using AutoMapper;
using Events.Application.DTO;
using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using Moq;

namespace TestEventApplication.Services
{
    public class LocationServiceTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper mapper;

        public LocationServiceTests()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingProfile>();
            });
            mapper = config.CreateMapper();

            _unitOfWorkMock.Setup(uow => uow.LocationRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetLocationAsync_ReturnsException()
        {
            var service = new LocationService(_unitOfWorkMock.Object, mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                service.GetLocationAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetLocationAsync()
        {
            // Arrange
            var service = new LocationService(_unitOfWorkMock.Object, mapper);
            var locationRequest = GetLocationRequest();
            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetLocationObject());

            // Act
            int id = await service.InsertLocationAsync(locationRequest, CancellationToken.None);
            var location = await service.GetLocationAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(location);
            Assert.Equal(locationRequest.Name, location.Name);
        }

        [Fact]
        public async Task GetAllLocationsAsync()
        {
            // Arrange
            var service = new LocationService(_unitOfWorkMock.Object, mapper);
            var locationsList = new List<Location>
            {
                new Location { Id = 1, StreetId = 1, Name = "First Category", House = "44" },
                new Location { Id = 2, StreetId = 2, Name = "Second", House = "1" },
                new Location { Id = 3, StreetId = 1, Name = "Third", House = "3"}
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(locationsList);

            // Act
            var locations = await service.GetAllLocationsAsync(CancellationToken.None);

            // Assert
            Assert.True(locations.Count() == locationsList.Count);
        }

        [Fact]
        public async Task InsertLocation()
        {
            // Arrange
            var service = new LocationService(_unitOfWorkMock.Object, mapper);
            var locationRequest = GetLocationRequest();

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await service.InsertLocationAsync(locationRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task UpdateLocation()
        {
            // Arrange
            var service = new LocationService(_unitOfWorkMock.Object, mapper);

            var oldLocation = GetLocationObject();
            var updatedLocationDto = GetLocationRequest();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(oldLocation.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(oldLocation);

            // Act
            await service.UpdateLocationAsync(oldLocation.Id, updatedLocationDto, CancellationToken.None);
            var location = await service.GetLocationAsync(oldLocation.Id, CancellationToken.None);

            // Assert
            Assert.True(location.Id == oldLocation.Id);
            Assert.True(location.Name == updatedLocationDto.Name);
        }

        [Fact]
        public async Task DeleteLocationAsync()
        {
            // Arrange
            var service = new LocationService(_unitOfWorkMock.Object, mapper);

            var existingLocation = GetLocationObject();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(existingLocation.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingLocation);

            // Act
            await service.DeleteLocationAsync(existingLocation.Id, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.Is<Location>(e => e.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private Location GetLocationObject()
        {
            return new Location
            {
                Id = 1,
                StreetId = 1,
                Name = "Location",
                House = "11"
            };
        }

        private LocationRequestDto GetLocationRequest()
        {
            return new LocationRequestDto
            {
                StreetId = 1,
                Name = "Location",
                House = "11"
            };
        }
    }
}
