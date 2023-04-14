using RoverHello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverHello.Domain.Entities
{
    public class Attendee
    {
        [Key]
        public int AttendeeId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
