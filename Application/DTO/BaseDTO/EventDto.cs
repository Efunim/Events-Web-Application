namespace Events.Application.DTO.BaseDTO
{
    public class EventDto
    {
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public int MaxParticipants { get; set; }
        public string? ImagePath { get; set; } = "";
    }
}
