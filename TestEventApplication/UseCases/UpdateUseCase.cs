using AutoMapper;
using Events.Application.DTO.MappingProfiles;
using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCases;
using Events.Application.UseCases;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Moq;

namespace TestEventApplication.UseCases
{
    public class UpdateUseCase
    {
        private readonly Mock<IParticipantRepository> _participantRepositoryMock;
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly IMapper _mapper;

        public UpdateUseCase()
        {
            _participantRepositoryMock = new();
            _locationRepositoryMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ParticipantMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task UpdateParticipant()
        {
            // Arrange
            var participantRepositoryMock = new Mock<IParticipantRepository>();
            var mapperMock = new Mock<IMapper>();
            var validateUseCaseMock = new Mock<IValidationUseCase<ParticipantRequestDto>>();

            var updateUseCase = new UpdateParticipantUseCase(
                participantRepositoryMock.Object,
                mapperMock.Object
            );

            int id = 1;
            var participantRequest = ObjectsGen.GetParticipantRequest();
            var updatedParticipant = ObjectsGen.GetParticipantObject();
            updatedParticipant.EventId = 3;

            participantRepositoryMock.Setup(repo => repo.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                                     .ReturnsAsync(updatedParticipant);

            validateUseCaseMock.Setup(validation => validation.ExecuteAsync(It.IsAny<ParticipantRequestDto>(), It.IsAny<CancellationToken>()))
                               .Returns(Task.CompletedTask);

            // Act
            await updateUseCase.ExecuteAsync(id, participantRequest, CancellationToken.None);

            // Assert
            participantRepositoryMock.Verify(repo => repo.Update(updatedParticipant), Times.Once);
        }

        [Fact]
        public async Task UpdateParticipant_ReturnsException()
        {
            // Arrange
            var updateUseCase = new AddLocationUseCase(_locationRepositoryMock.Object, _mapper);
            var locationRequest = ObjectsGen.GetLocationRequest();
            locationRequest.House = "";

            _participantRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            await Assert.ThrowsAsync<ValidationException>(() =>
                updateUseCase.ExecuteAsync(locationRequest, CancellationToken.None));
        }
    }
}
