using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SED.Models
{
    public class Email
    {
        [Required(ErrorMessage = "Please specify a sport event")]
        public int SportEventId { get; set; }
        [Required(ErrorMessage = "Please enter an subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter an message")]
        public string Body { get; set; }
    }
}
