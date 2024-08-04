namespace Events.Application.Filters
{
    public class EventFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LocationFilter? Location { get; set; }
        public int? CategoryId { get; set; }
    }
}