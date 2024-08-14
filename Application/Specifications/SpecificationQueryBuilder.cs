using Events.Domain.Entities;

namespace Events.Application.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        Specification<TEntity> specification,
        int pageIndex = 0,
        int pageSize = 2)
        where TEntity : BaseEntity
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = query.Skip(pageIndex * pageSize).Take(pageSize);

            return query;
        }
    }
}
