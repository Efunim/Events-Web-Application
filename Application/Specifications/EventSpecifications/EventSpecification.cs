using Events.Domain.Entities;

namespace Events.Application.Specifications.EventSpecifications
{
    public class EventSpecification : Specification<Event>
    {
        public EventSpecification(
            IEnumerable<int>? categoryIds = null, string name = "", int? cityId = null,
            DateTime? startDate = null, DateTime? endDate = null)
            : base(e =>
                (categoryIds == null || !categoryIds.Any() || categoryIds.Contains(e.CategoryId)) &&
                (string.IsNullOrEmpty(name) || e.Name.ToLower().Contains(name.ToLower())) &&
                (cityId == null || e.Location.Street.CityId == cityId) &&
                (!startDate.HasValue || e.EventTime.Date >= startDate.Value.Date) &&
                (!endDate.HasValue || e.EventTime.Date <= endDate.Value.Date))
        { }
    }
}
