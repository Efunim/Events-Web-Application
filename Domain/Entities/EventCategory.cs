using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class EventCategory : BaseEntity
    {
        public string Name { get; set; }

        public List<Event> Events { get; private set; }
    }
}
