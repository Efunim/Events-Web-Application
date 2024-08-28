using AutoMapper;
using Events.Application.DTO.MappingProfiles;
using Events.Application.Exceptions;
using Events.Application.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Moq;

namespace TestEventApplication.UseCases
{
    public class GetEntityUseCase
    {
        private readonly Mock<IEventRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public GetEntityUseCase()
        {
            _repositoryMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetEventAsync_ReturnsException()
        {
            var getUseCase = new GetEventUseCase(_repositoryMock.Object, _mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                getUseCase.ExecuteAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetEventAsync()
        {
            // Arrange
            var getUseCase = new GetEventUseCase(_repositoryMock.Object, _mapper);
            var eventToGet = ObjectsGen.GetEventObject();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(eventToGet);

            // Act
            var @event = await getUseCase.ExecuteAsync(eventToGet.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(@event);
            Assert.Equal(eventToGet.Name, @event.Name);
        }
    }
}
