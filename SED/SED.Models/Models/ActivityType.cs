using System.Collections.Generic;

namespace SED.Models
{
    public class ActivityType
    {
        public int ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
