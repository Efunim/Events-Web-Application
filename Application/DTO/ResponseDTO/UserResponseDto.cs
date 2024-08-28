using Events.Application.DTO.BaseDTO;

namespace Events.Application.DTO.ResponseDTO
{
    public class UserResponseDto : UserDto
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
    }
}
