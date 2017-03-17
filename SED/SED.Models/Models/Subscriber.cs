using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Models
{
    public class Subscriber
    {
        public int SubscriberId { get; set; }
        public int SportEventId { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public virtual SportEvent SportEvent { get; set; }
    }
}
