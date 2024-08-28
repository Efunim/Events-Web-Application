using Events.Domain.Entities;

namespace Events.Domain.Specifications.EventSpecifications
{
    public class EventCitySpecification : ISpecification<Event>
    {
        private readonly int? _cityId;

        public EventCitySpecification(int? cityId)
        {
            _cityId = cityId;
        }

        public bool IsSatisfiedBy(Event entity)
        {
            return !_cityId.HasValue || entity.Location.Street.CityId == _cityId.Value;
        }

        public IQueryable<Event> Apply(IQueryable<Event> query)
        {
            if (_cityId.HasValue)
            {
                query = query.Where(e => e.Location.Street.CityId == _cityId.Value);
            }
            return query;
        }
    }
}
