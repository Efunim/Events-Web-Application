using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class City : BaseEntity
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public Country Country { get; set; }
        public List<Street> Streets { get; private set; } = [];
    }
}
