namespace SED.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int SportEventId { get; set; }
        public int ActivityTypeId { get; set; }
        public string Name { get; set; }

        public virtual SportEvent SportEvent { get; set; }
        public virtual ActivityType ActivityType { get; set; }
    }
}
