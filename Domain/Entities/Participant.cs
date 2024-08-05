namespace Events.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
