using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRandomRPG.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status{ get; set; }
        public Reward Reward{ get; set; }
    }
}
