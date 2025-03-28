using Events.Domain.Entities;
using System.Linq.Expressions;

namespace Events.Application.Specifications
{
    public abstract class Specification<TEntity> where TEntity : BaseEntity
    {
        public Specification() { }

        public Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

    }
}
