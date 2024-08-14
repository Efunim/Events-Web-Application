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
    public class ParticipantServiceTests
    {
        private readonly Mock<IParticipantRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper mapper;

        public ParticipantServiceTests()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingProfile>();
            });
            mapper = config.CreateMapper();

            _unitOfWorkMock.Setup(uow => uow.ParticipantRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetParticipantAsync_ReturnsException()
        {
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                service.GetParticipantAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetParticipantAsync()
        {
            // Arrange
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);
            var participantRequest = GetParticipantRequest();
            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetParticipantObject());

            // Act
            int id = await service.InsertParticipantAsync(participantRequest, CancellationToken.None);
            var participant = await service.GetParticipantAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(participant);
            Assert.True(participant.EventId == participantRequest.EventId);
            Assert.True(participant.UserId == participantRequest.UserId);
        }

        [Fact]
        public async Task GetAllParticipantsAsync()
        {
            // Arrange
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);
            var participantsList = new List<Participant>
            {
                new Participant { Id = 1, EventId = 1, UserId = 1, RegistrationDate = DateTime.Now },
                new Participant { Id = 2, EventId = 2, UserId = 2, RegistrationDate = DateTime.Now },
                new Participant { Id = 3, EventId = 3, UserId = 3, RegistrationDate = DateTime.Now}
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(participantsList);

            // Act
            var participants = await service.GetAllParticipantsAsync(CancellationToken.None);

            // Assert
            Assert.True(participants.Count() == participantsList.Count);
        }

        [Fact]
        public async Task InsertParticipant()
        {
            // Arrange
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);
            var participantRequest = GetParticipantRequest();

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await service.InsertParticipantAsync(participantRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task UpdateParticipant()
        {
            // Arrange
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);

            var oldParticipant = GetParticipantObject();
            var updatedParticipantDto = GetParticipantRequest();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(oldParticipant.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(oldParticipant);

            // Act
            await service.UpdateParticipantAsync(oldParticipant.Id, updatedParticipantDto, CancellationToken.None);
            var participant = await service.GetParticipantAsync(oldParticipant.Id, CancellationToken.None);

            // Assert
            Assert.True(participant.Id == oldParticipant.Id);
            Assert.True(participant.EventId == updatedParticipantDto.EventId);
            Assert.True(participant.UserId == updatedParticipantDto.UserId);
        }

        [Fact]
        public async Task DeleteParticipantAsync()
        {
            // Arrange
            var service = new ParticipantService(_unitOfWorkMock.Object, mapper);

            var existingParticipant = GetParticipantObject();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(existingParticipant.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingParticipant);

            // Act
            await service.DeleteParticipantAsync(existingParticipant.Id, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.Is<Participant>(e => e.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private Participant GetParticipantObject()
        {
            return new Participant
            {
                Id = 1,
                EventId = 1,
                UserId = 1,
                RegistrationDate = DateTime.UtcNow,
            };
        }

        private ParticipantRequestDto GetParticipantRequest()
        {
            return new ParticipantRequestDto
            {
                EventId = 1,
                UserId = 1,
                RegistrationDate = DateTime.UtcNow,
            };
        }
    }
}
