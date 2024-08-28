using AutoMapper;
using Events.Application.DTO.MappingProfiles;
using Events.Application.Exceptions;
using Events.Application.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Moq;

namespace TestEventApplication.UseCases
{
    public class DeleteEntityUseCase
    {
        private readonly Mock<IParticipantRepository> _participantRepositoryMock;
        private readonly Mock<IEventCategoryRepository> _eventCategoryRepositoryMock;
        private readonly IMapper mapper;

        public DeleteEntityUseCase()
        {
            _participantRepositoryMock = new();
            _eventCategoryRepositoryMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ParticipantMappingProfile>();
                cfg.AddProfile<EventCategoryMappingProfile>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task DeleteEventCategoryAsync()
        {
            // Arrange
            var deleteUseCase = new DeleteEventCategoryUseCase(_eventCategoryRepositoryMock.Object);
            var existingParticipant = ObjectsGen.GetEventCategoryObject();

            _eventCategoryRepositoryMock.Setup(repo => repo.GetByIdAsync(existingParticipant.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingParticipant);

            // Act
            await deleteUseCase.ExecuteAsync(existingParticipant.Id, CancellationToken.None);

            // Assert
            _eventCategoryRepositoryMock.Verify(repo => repo.Delete(It.Is<EventCategory>(e => e.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task DeleteParticipantAsync_ReturnsException()
        {
            // Arrange
            var deleteUseCase = new DeleteParticipantUseCase(_participantRepositoryMock.Object);
            var existingParticipant = ObjectsGen.GetParticipantObject();

            _participantRepositoryMock.Setup(repo => repo.GetByIdAsync(existingParticipant.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingParticipant);

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                deleteUseCase.ExecuteAsync(2, CancellationToken.None));
        }

    }
}
