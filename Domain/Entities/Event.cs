using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class Event : BaseEntity
    {
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public int MaxParticipants { get; set; }
        public string ImagePath { get; set; }

        public Location Location { get; set; }
        public EventCategory Category { get; set; }
        public List<Participant> Participants { get; private set; } = [];
    }
}
