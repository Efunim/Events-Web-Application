using Events.Application.DTO.ResponseDTO;

namespace Events.Application
{
    public class AuthenticateResponse
    {
        public UserResponseDto User { get; set; }
        public string Token { get; set; }
    }
}
