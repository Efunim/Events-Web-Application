using Events.Application.DTO.BaseDTO;

namespace Events.Application.DTO.ResponseDTO
{
    public class UserResponseDto : UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
