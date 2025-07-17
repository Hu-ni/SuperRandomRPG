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
        public int Level { get; set; }
        public Status Status { get; set; }
        public Reward Reward { get; set; }

        public int BaseHealth { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int HealthPerLvl { get; set; }
        public int AttackPerLvl { get; set; }
        public int DefensePerLvl { get; set; }

        public Monster Clone()
        {
            return new Monster
            {
                Id = this.Id,
                Name = this.Name,
                Level = this.Level,
                BaseHealth = this.BaseHealth,
                BaseAttack = this.BaseAttack,
                BaseDefense = this.BaseDefense,
                HealthPerLvl = this.HealthPerLvl,
                AttackPerLvl = this.AttackPerLvl,
                DefensePerLvl = this.DefensePerLvl,
                Status = this.Status != null ? new Status
                {
                    Health = this.Status.Health,
                    Attack = this.Status.Attack,
                    Defense = this.Status.Defense
                } : new Status(),
                Reward = this.Reward != null ? new Reward
                {
                    Exp = this.Reward.Exp,
                    Money = this.Reward.Money
                } : new Reward()
            };
        }
    }
}
