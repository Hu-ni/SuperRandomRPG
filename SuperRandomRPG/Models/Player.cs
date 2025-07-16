using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public Player(string name, Job job, Status status, int level = 1, int exp = 0, int health = 100, int mana = 50, int gold = 1000, int luck = 0)
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

        public void OpenStatus()
        {
            Console.WriteLine("플레이어 상태창입니다. 0을 눌러 나갈 수 있습니다.");
            Console.WriteLine($"이름: {Name}");
            Console.WriteLine($"직업: {Job}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Experience: {Experience}");
            Console.WriteLine($"체력: {Health}");
            Console.WriteLine($"마나: {Mana}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"행운: {Luck}");
            Console.WriteLine("Status:");
            Console.WriteLine($"공격력: {Status.Attack}");
            Console.WriteLine($"방어력: {Status.Defense}");
            Console.WriteLine($"0. 나가기");

            while(true)
            {
                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
    }
}
