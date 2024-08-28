using Events.Domain.Entities;

namespace Events.Domain.Specifications.EventSpecifications
{
    public class EventDateSpecification : ISpecification<Event>
    {
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;

        public EventDateSpecification(DateTime? startDate, DateTime? endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public bool IsSatisfiedBy(Event entity)
        {
            bool satisfiesStartDate = !_startDate.HasValue || entity.EventTime >= _startDate.Value;
            bool satisfiesEndDate = !_endDate.HasValue || entity.EventTime <= _endDate.Value;
            return satisfiesStartDate && satisfiesEndDate;
        }

        public IQueryable<Event> Apply(IQueryable<Event> query)
        {
            if (_startDate.HasValue)
            {
                query = query.Where(e => e.EventTime >= _startDate.Value);
            }
            if (_endDate.HasValue)
            {
                query = query.Where(e => e.EventTime <= _endDate.Value);
            }
            return query;
        }
    }
}
