using Events.Application.Exceptions;
using Events.Domain.Entities;

namespace Events.Application.Services
{
    public class ServiceHelper
    {
        public static async Task<T> GetEntityAsync<T>
            (Func<int, CancellationToken, Task<T?>> getByIdAsync, int id, CancellationToken cancellationToken)
             where T : BaseEntity
        {
            var data = await getByIdAsync(id, cancellationToken);
            if (data == null)
            {
                throw new ObjectNotFoundException($"Requested object: {typeof(T)}, id {id}");
            }

            return data;
        }
    }
}
