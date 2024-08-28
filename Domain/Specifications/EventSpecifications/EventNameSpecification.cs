using Events.Domain.Entities;

namespace Events.Domain.Specifications.EventSpecifications
{
    public class EventNameSpecification : ISpecification<Event>
    {
        private readonly string _name;

        public EventNameSpecification(string name)
        {
            _name = name;
        }

        public bool IsSatisfiedBy(Event entity)
        {
            return string.IsNullOrEmpty(_name) || entity.Name.ToLower().Contains(_name.ToLower());
        }

        public IQueryable<Event> Apply(IQueryable<Event> query)
        {
            if (!string.IsNullOrEmpty(_name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(_name.ToLower()));
            }
            return query;
        }
    }
}
