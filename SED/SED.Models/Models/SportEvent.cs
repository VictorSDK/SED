using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SED.Models
{
    public class SportEvent
    {        
        public int SportEventId { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a date")]
        public DateTime? Date { get; set; }
        public int Rating { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Subscriber> Subscribers { get; set; }
    }
}
