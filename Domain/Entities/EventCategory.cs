namespace Events.Domain.Entities
{
    public class EventCategory : BaseEntity
    {
        public string Name { get; set; }

        public List<Event> Events { get; private set; }
    }
}
