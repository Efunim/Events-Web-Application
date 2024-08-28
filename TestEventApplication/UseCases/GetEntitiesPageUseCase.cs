using AutoMapper;
using Events.Application.DTO.MappingProfiles;
using Events.Application.UseCases;
using Events.Domain.Repositories;
using Moq;

namespace TestEventApplication.UseCases
{
    public class GetEntitiesPageUseCase
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public GetEntitiesPageUseCase()
        {
            _repositoryMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetUsersPage()
        {
            // Arrange
            var getPageUseCase = new GetUsersPageUseCase(_repositoryMock.Object, _mapper);
            var usersList = ObjectsGen.GetUsersList();
            int pageIndex = 0;
            int pageSize = 2;
            _repositoryMock.Setup(repo => repo.GetPageAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync(usersList.Skip(pageIndex*pageSize).Take(pageSize).ToList());

            // Act
            var page = await getPageUseCase.ExecuteAsync(pageIndex, pageSize, CancellationToken.None);

            // Assert
            Assert.True(page.Count() == pageSize);
        }
    }
}
