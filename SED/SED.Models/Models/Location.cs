using System.Collections.Generic;

namespace SED.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SportEvent> SportEvents { get; set; }
    }
}
