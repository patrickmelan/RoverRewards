using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RoverHello.Domain.Entities
{
	[Keyless]
	public class Event {

		//[Key]
		
		public int Id { get; set; }
        public DateTime Date { get; set;}
        public string Name { get; set; }

        public int Points { get; set; }
        public string Description { get; set; }

        public List<string> Attendees {  get; set; }

    }
}
