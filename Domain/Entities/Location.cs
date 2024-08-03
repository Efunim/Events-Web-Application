using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class Location : BaseEntity
    {
        public int StreetId { get; set; }
        public string? Name { get; set; }
        public string House { get; set; }

        public Street Street { get; set; }
        public List<Event> Events { get; private set; } = [];
    }
}
