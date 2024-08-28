namespace Events.Domain.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        IQueryable<T> Apply(IQueryable<T> query);
    }

}
