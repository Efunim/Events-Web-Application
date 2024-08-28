using System.Linq.Expressions;

namespace Events.Domain.Specifications
{
    public class CompositeSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T>[] _specifications;

        public CompositeSpecification(params ISpecification<T>[] specifications)
        {
            _specifications = specifications;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _specifications.All(spec => spec == null || spec.IsSatisfiedBy(entity));
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            foreach (var spec in _specifications)
            {
                if (spec != null)
                {
                    query = spec.Apply(query);
                }
            }
            return query;
        }
    }
}
