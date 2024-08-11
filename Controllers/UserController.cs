using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRequestDto userDto, CancellationToken token)
        {
            var userId = await userService.InsertUserAsync(userDto, token);
            var userResponse = await userService.GetUserAsync(userId, token);

            return await Login(userDto, token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRequestDto userDto, CancellationToken token)
        {
            var response = await userService.AuthenticateAsync(userDto, token);
            if (response == null) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet(Name = "GetUser")]
        public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken) =>
            await userService.GetUserAsync(id, cancellationToken);

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken) =>
            await userService.GetAllUsersAsync(cancellationToken);

        [HttpGet(Name = "InsertUser")]
        public async Task<int> InsertUserAsync(UserRequestDto UserDto, CancellationToken cancellationToken) =>
            await userService.InsertUserAsync(UserDto, cancellationToken);

        [HttpGet(Name = "UpdateUser")]
        public async Task UpdateUserAsync(int id, UserRequestDto UserDto, CancellationToken cancellationToken) =>
            await userService.UpdateUserAsync(id, UserDto, cancellationToken);

        [HttpGet(Name = "DeleteUser")]
        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken) =>
            await userService.DeleteUserAsync(id, cancellationToken);
    }
}
