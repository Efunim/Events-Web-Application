using AutoMapper;
using Events.Application;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Repositories;
using Events.Infastructure.Authentification;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IAddUserUseCase _addUseCase;
        private readonly IDeleteUserUseCase _deleteUseCase;
        private readonly IGetUserUseCase _getUseCase;
        private readonly IGetUsersPageUseCase _getPageUseCase;
        private readonly IUpdateUserUseCase _updateUseCase;
        private readonly IStoreRefreshTokenUseCase _storeRefreshTokenUseCase;
        private readonly IAuthentificationService _authService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        
        public UserController(
        IAddUserUseCase addUseCase,
        IDeleteUserUseCase deleteUseCase,
        IGetUserUseCase getUseCase,
        IGetUsersPageUseCase getPageUseCase,
        IUpdateUserUseCase updateUseCase,
        IStoreRefreshTokenUseCase storeRefreshTokenUseCase,
        IAuthentificationService authService,
        IUnitOfWork uow,
        IMapper mapper)
        {
            _addUseCase = addUseCase;
            _deleteUseCase = deleteUseCase;
            _getUseCase = getUseCase;
            _getPageUseCase = getPageUseCase;
            _updateUseCase = updateUseCase;
            _storeRefreshTokenUseCase = storeRefreshTokenUseCase;
            _authService = authService;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            var originalPassword = userDto.Password;
            userDto.Password = _authService.HashPassword(userDto.Password);
            var userId = await this.InsertUserAsync(userDto, cancellationToken);

            userDto.Password = originalPassword;
            return await Login(userDto, cancellationToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            AuthenticateResponse? response = await LoginByRefreshToken(cancellationToken);

            if (response == null)
            {
                response = await LoginByJwt(userDto, cancellationToken);
            }

            if (response == null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet(Name = "GetUser")]
        public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken) =>
            await _getUseCase.ExecuteAsync(id, cancellationToken);

        [HttpGet(Name = "GetUsersPage")]
        public async Task<IEnumerable<UserResponseDto>> GetUsersPageAsync(CancellationToken cancellationToken,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 2) =>
            await _getPageUseCase.ExecuteAsync(pageIndex, pageSize, cancellationToken);

        [HttpPost(Name = "InsertUser")]
        public async Task<int> InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            int id = await _addUseCase.ExecuteAsync(userDto, cancellationToken);
            await _uow.SaveAsync(cancellationToken);

            return id;
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
        {
            await _updateUseCase.ExecuteAsync(id, userDto, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            await _deleteUseCase.ExecuteAsync(id, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        private async Task<AuthenticateResponse?> LoginByRefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            return await _authService.ValidateRefreshTokenAsync(refreshToken, cancellationToken);
        }

        private async Task<AuthenticateResponse?> LoginByJwt(UserRequestDto userDto, CancellationToken cancellationToken)
        {
            var response = await _authService.AuthenticateAsync(userDto, cancellationToken);
            if(response != null) SetRefreshTokenInCookies(response.RefreshToken);

            return response;
        }

        private void SetRefreshTokenInCookies(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(2)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
