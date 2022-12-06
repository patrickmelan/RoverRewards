using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverHello.Domain.Entities
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Point Cost")]
        public int PointCost { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int Count { get; set; }
    }
}
