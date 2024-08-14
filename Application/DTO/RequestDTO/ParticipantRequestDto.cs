namespace Events.Application.DTO.RequestDTO
{
    public class ParticipantRequestDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
