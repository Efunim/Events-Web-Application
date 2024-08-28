using Events.Application.DTO.BaseDTO;

namespace Events.Application.DTO.RequestDTO
{
    public class UserRequestDto : UserDto
    {
        public string Password { get; set; }
    }
}
