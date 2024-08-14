using AutoMapper;
using Events.Application.DTO;
using Events.Application.DTO.RequestDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEventApplication.Services
{
    public class EventCategoryServiceTests
    {
        private readonly Mock<IEventCategoryRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper mapper;

        public EventCategoryServiceTests()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingProfile>();
            });
            mapper = config.CreateMapper();

            _unitOfWorkMock.Setup(uow => uow.EventCategoryRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetEventCategoryAsync_ReturnsException()
        {
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);
            int id = 1;

            await Assert.ThrowsAsync<ObjectNotFoundException>(() =>
                service.GetEventCategoryAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetEventCategoryAsync()
        {
            // Arrange
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);
            var eventCategoryRequest = new EventCategoryRequestDto
            {
                Name = "Category"
            };
            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<EventCategory>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EventCategory { Id = 1, Name = eventCategoryRequest.Name });

            // Act
            int id = await service.InsertEventCategoryAsync(eventCategoryRequest, CancellationToken.None);
            var eventCategory = await service.GetEventCategoryAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(eventCategory);
            Assert.Equal(eventCategoryRequest.Name, eventCategory.Name);
        }

        [Fact]
        public async Task GetAllEventsAsync()
        {
            // Arrange
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);
            var eventCategories = new List<EventCategory>
            {
                new EventCategory { Id = 1, Name = "First Category" },
                new EventCategory { Id = 2, Name = "Second" },
                new EventCategory { Id = 3, Name = "Third"}
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(eventCategories);

            // Act
            var categories = await service.GetAllEventCategoriesAsync(CancellationToken.None);

            // Assert
            Assert.True(categories.Count() == eventCategories.Count);
        }

        [Fact]
        public async Task InsertEventCategory()
        {
            // Arrange
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);
            var eventCategoryRequest = new EventCategoryRequestDto
            {
                Name = "Category"
            };

            _repositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<EventCategory>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            int id = await service.InsertEventCategoryAsync(eventCategoryRequest, CancellationToken.None);

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public async Task UpdateEventCategory()
        {
            // Arrange
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);

            var oldCategory = new EventCategory { Id = 1, Name = "Old Category" };
            var updatedCategoryDto = new EventCategoryRequestDto { Name = "Updated Category" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(oldCategory.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(oldCategory);

            // Act
            await service.UpdateEventCategoryAsync(oldCategory.Id, updatedCategoryDto, CancellationToken.None);
            var category = await service.GetEventCategoryAsync(oldCategory.Id, CancellationToken.None);

            // Assert
            Assert.True(category.Id == oldCategory.Id);
            Assert.True(category.Name == updatedCategoryDto.Name);
        }

        [Fact]
        public async Task DeleteCategoryAsync()
        {
            // Arrange
            var service = new EventCategoryService(_unitOfWorkMock.Object, mapper);

            var existingCategory = new EventCategory { Id = 1, Name = "Category to Delete" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCategory);

            // Act
            await service.DeleteEventCategoryAsync(1, CancellationToken.None);
          
            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.Is<EventCategory>(e => e.Id == 1)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
