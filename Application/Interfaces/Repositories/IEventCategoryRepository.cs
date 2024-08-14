using Events.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces.Repositories
{
    public interface IEventCategoryRepository : IGenericRepository<EventCategory>
    {
    }
}
