using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Validators;
using Events.Domain.Entities;

namespace Events.Application.Services
{
    public class UserService(IUnitOfWork uow, IMapper mapper) : IUserService
    {
        private readonly IUserRepository repository = uow.UserRepository;

        public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            return mapper.Map<User, UserResponseDto>(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users);
        }

        public async Task<int> InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            await ValidateUser(userDto, cancellationToken);
            var user = mapper.Map<UserRequestDto, User>(userDto);

            var id = await repository.InsertAsync(user, cancellationToken);
            await uow.SaveAsync(cancellationToken);

            return id;
        }

        public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
        {
            await ValidateUser(userDto, cancellationToken);

            var user = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            mapper.Map(userDto, user);
            repository.Update(user);

            await uow.SaveAsync(cancellationToken);
        }

        public async Task StoreRefreshTokenAsync(int id, string refreshToken, CancellationToken cancellationToken)
        {
            var user = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            repository.Update(user);
            await uow.SaveAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            repository.Delete(user);

            await uow.SaveAsync(cancellationToken);
        }

        private async Task ValidateUser(UserRequestDto user, CancellationToken cancellationToken)
        {
            UserRequestDtoValidator validator = new UserRequestDtoValidator();
            var results = await validator.ValidateAsync(user);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
