using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Repositories;
using Events.Domain.Entities;
using Events.Infastructure.Authentification;
using Moq;

namespace TestEventApplication.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthentificationService service;

        public AuthServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_repositoryMock.Object);

            service = new AuthentificationService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsNull()
        {
            // Arrange
            var userRequest = GetUserRequest();
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await service.AuthenticateAsync(userRequest, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AuthenticateAsync()
        {
            // Arrange
            var userRequest = GetUserRequest();
            var user = GetUserObject();
            user.PasswordHash = service.HashPassword(userRequest.Password);

            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User> { user });

            _mapperMock.Setup(m => m.Map<UserResponseDto>(user))
                .Returns(new UserResponseDto
                {
                    Id = user.Id,
                    Name = "Name",
                    Surname = "Surname",
                    Birthday = DateTime.UtcNow.AddYears(-20),
                    Email = "a@gmail.com",
                });

            // Act
            var result = await service.AuthenticateAsync(userRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.User.Id);
        }

        [Fact]
        public async Task ValidateRefreshTokenAsync_ReturnsNull()
        {
            // Arrange
            var refreshToken = "invalidToken";
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await service.ValidateRefreshTokenAsync(refreshToken, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ValidateRefreshTokenAsync()
        {
            // Arrange
            var user = GetUserObject();
            user.RefreshToken = "validToken";
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User> { user });

            _mapperMock.Setup(m => m.Map<UserResponseDto>(user)).Returns(new UserResponseDto { Id = user.Id });

            // Act
            var result = await service.ValidateRefreshTokenAsync(user.RefreshToken, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(user.Id == result.User.Id);
        }

        [Fact]
        public void HashPassword_ReturnsCorrectHash()
        {
            // Arrange
            var password = "password";

            // Act
            var hash = service.HashPassword(password);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEqual(password, hash);
        }

        private User GetUserObject()
        {
            return new User
            {
                Id = 1,
                Name = "Name",
                Surname = "Surname",
                Birthday = DateTime.UtcNow.AddYears(-20),
                Email = "m@gmail.com",
                PasswordHash = "passwordHash"
            };
        }

        private UserRequestDto GetUserRequest()
        {
            return new UserRequestDto
            {
                Email = "m@gmail.com",
                Password = "password"
            };
        }
    }
}