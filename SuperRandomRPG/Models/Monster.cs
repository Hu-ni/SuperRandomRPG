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
        public Status Status { get; set; }
        public Reward Reward { get; set; }

        public Monster Clone()
        {
            return new Monster
            {
                Id = this.Id,
                Name = this.Name,
                Status = new Status
                {
                    Health = this.Status.Health,
                    Attack = this.Status.Attack,
                    Defense = this.Status.Defense
                },
                Reward = new Reward
                {
                    Exp = this.Reward.Exp,
                    Money = this.Reward.Money
                }
            };
        }
    }
}
