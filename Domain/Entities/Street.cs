using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class Street : BaseEntity
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }

        public City City { get; set; }
        public List<Location> Locations { get; private set; } = [];
    }
}
