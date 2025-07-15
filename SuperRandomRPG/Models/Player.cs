using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SRRPG.Model
{
    public enum Job
    {
        Warrior,
        Mage,
        Archer
    }

    public class Player
    {
        public string Name { get; set; } // 플레이어 이름
        public Job Job { get; set; } // 플레이어 직업
        public int Level { get; set; } // 플레이어 레벨
        public int Experience { get; set; } // 플레이어 경험치
        public Status Status { get; set; }
        public int Health { get; set; } // 플레이어 체력
        public int Mana { get; set; } // 플레이어 마나
        public int Gold { get; set; } // 플레이어 소지 금액
        public int Luck { get; set; } // 플레이어 행운
        public Player(string name, Job job, int level = 1, int exp = 0, Status status int health = 100, int mana = 50, int gold = 1000, int luck = 0)
        {
            Name = name;
            Job = job;
            Level = level;
            Experience = exp;
            Status = status;
            Health = health;
            Mana = mana;
            Gold = gold;
            Luck = luck;
        }
    }
}
