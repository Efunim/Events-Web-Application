using Events.Domain.Entities;

namespace Events.Domain.Specifications.EventSpecifications
{
    public class EventCategorySpecification : ISpecification<Event>
    {
        private readonly IEnumerable<int>? _categoryIds;

        public EventCategorySpecification(IEnumerable<int>? categoryIds)
        {
            _categoryIds = categoryIds;
        }

        public bool IsSatisfiedBy(Event entity)
        {
            // Если _categoryIds не задано или пусто, возвращаем true (все записи соответствуют).
            return _categoryIds == null || !_categoryIds.Any() || _categoryIds.Contains(entity.CategoryId);
        }

        public IQueryable<Event> Apply(IQueryable<Event> query)
        {
            if (_categoryIds != null && _categoryIds.Any())
            {
                query = query.Where(e => _categoryIds.Contains(e.CategoryId));
            }
            return query;
        }
    }
}
