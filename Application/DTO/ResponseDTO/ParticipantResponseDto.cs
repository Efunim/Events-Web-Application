namespace Events.Application.DTO.ResponseDTO
{
    public class ParticipantResponseDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
