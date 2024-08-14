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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper mapper;

        public UserServiceTests()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingProfile>();
            });
            mapper = config.CreateMapper();

            _unitOfWorkMock.Setup(uow => uow.UserRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetUserAsync_ReturnsException()
        {
            var service = new UserService(_unitOfWorkMock.Object, mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                service.GetUserAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetUserAsync()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);
            var userRequest = GetUserRequest();
            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetUserObject());

            // Act
            int id = await service.InsertUserAsync(userRequest, CancellationToken.None);
            var user = await service.GetUserAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(userRequest.Name, user.Name);
        }

        [Fact]
        public async Task GetAllUsersAsync()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);
            var usersList = new List<User>
            {
                new User
                {
                    Id = 1, 
                    Name = "One", 
                    Surname = "One", 
                    Birthday = DateTime.Now.AddYears(-20), 
                    Email = "m@gmail.com",
                    IsAdmin = true,
                    PasswordHash = "hash"
                },
                new User
                {
                    Id = 2,
                    Name = "Two",
                    Surname = "Two",
                    Birthday = DateTime.Now.AddYears(-20),
                    Email = "t@gmail.com",
                    IsAdmin = false,
                    PasswordHash = "hash"
                },
                new User
                {
                    Id = 1,
                    Name = "Three",
                    Surname = "Three",
                    Birthday = DateTime.Now.AddYears(-20),
                    Email = "t@gmail.com",
                    IsAdmin = false,
                    PasswordHash = "hash"
                },
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(usersList);

            // Act
            var users = await service.GetAllUsersAsync(CancellationToken.None);

            // Assert
            Assert.True(users.Count() == usersList.Count);
        }

        [Fact]
        public async Task InsertUser()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);
            var userRequest = GetUserRequest();

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await service.InsertUserAsync(userRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task InsertUser_ReturnsValidationException()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);
            var userRequest = new UserRequestDto
            {
                Email = "2@gmail.com",
                Password = "1234"
            };

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.InsertUserAsync(userRequest, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateUser()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);

            var oldUser = GetUserObject();
            var updatedUserDto = GetUserRequest();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(oldUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(oldUser);

            // Act
            await service.UpdateUserAsync(oldUser.Id, updatedUserDto, CancellationToken.None);
            var user = await service.GetUserAsync(oldUser.Id, CancellationToken.None);

            // Assert
            Assert.True(user.Id == oldUser.Id);
            Assert.True(user.Name == updatedUserDto.Name);
        }

        [Fact]
        public async Task DeleteUserAsync()
        {
            // Arrange
            var service = new UserService(_unitOfWorkMock.Object, mapper);

            var existingUser = GetUserObject();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            // Act
            await service.DeleteUserAsync(existingUser.Id, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.Is<User>(e => e.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private User GetUserObject()
        {
            return new User
            {
                Id = 1,
                Name = "One",
                Surname = "One",
                Birthday = DateTime.Now.AddYears(-20),
                Email = "one@gmail.com",
                IsAdmin = true,
                PasswordHash = "hash"
            };
        }

        private UserRequestDto GetUserRequest()
        {
            return new UserRequestDto
            {
                Name = "One",
                Surname = "One",
                Birthday = DateTime.Now.AddYears(-20),
                Email = "m@gmail.com",
                Password = "password"
            };
        }
    }
}
