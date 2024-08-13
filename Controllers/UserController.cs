using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Services;
using Events.Application.Services;
using Events.Infastructure.Authentification;
using Events.Infastructure.Authentification.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(IUserService userService, IAuthentificationService authService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRequestDto userDto, CancellationToken token)
        {
            var originalPassword = userDto.Password;
            userDto.Password = authService.HashPassword(userDto.Password);
            var userId = await userService.InsertUserAsync(userDto, token);

            userDto.Password = originalPassword;
            return await Login(userDto, token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRequestDto userDto, CancellationToken token)
        {
            var response = await authService.AuthenticateAsync(userDto, token);
            if (response == null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet(Name = "GetUser")]
        public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken) =>
            await userService.GetUserAsync(id, cancellationToken);

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken) =>
            await userService.GetAllUsersAsync(cancellationToken);

        [HttpPost(Name = "InsertUser")]
        public async Task<int> InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            userDto.Password = authService.HashPassword(userDto.Password);
            return await userService.InsertUserAsync(userDto, cancellationToken);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
        {
            userDto.Password = authService.HashPassword(userDto.Password);
            await userService.UpdateUserAsync(id, userDto, cancellationToken);
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken) =>
            await userService.DeleteUserAsync(id, cancellationToken);
    }
}
