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
        Archer,
        Gambler
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


        public Player(string name, Job job, int level = 1, int exp = 0, int gold = 1000)
        {
            Name = name;
            Job = job;
            Level = level;
            Experience = exp;
            Gold = gold;

            switch (job)
            {
                case Job.Warrior:
                    Status = new Status { Attack = 15, Defense = 12, Health = 120, Mana = 120 };
                    Mana = Status.Mana;
                    Luck = 0;
                    Health = Status.Health; // 초기체력을 Status에서 가져옴
                    break;
                case Job.Mage:
                    Status = new Status { Attack = 10, Defense = 8, Health = 100, Mana = 150 };
                    Mana = Status.Mana;
                    Luck = 0;
                    Health = Status.Health; 
                    break;
                case Job.Archer:
                    Status = new Status { Attack = 12, Defense = 10, Health = 110, Mana = 100 };
                    Mana = Status.Mana;
                    Luck = 0;
                    Health = Status.Health; 
                    break;
                case Job.Gambler:
                    Status = new Status { Attack = 8, Defense = 6, Health = 90, Mana = 80 };
                    Mana = Status.Mana;
                    Luck = 10;
                    Health = Status.Health; 
                    break;
            }
        }

        public void LevelUp()
        {
            Level++;
            Experience = 0; // 레벨업 시 경험치 초기화
            switch(Job)
            {
                case Job.Warrior:
                    Status.Attack += 5;
                    Status.Defense += 3;
                    Status.Health += 10;
                    Status.Mana += 5;
                    break;
                case Job.Mage:
                    Status.Attack += 4;
                    Status.Defense += 2;
                    Status.Health += 8;
                    Status.Mana += 10;
                    break;
                case Job.Archer:
                    Status.Attack += 3;
                    Status.Defense += 2;
                    Status.Health += 7;
                    Status.Mana += 5;
                    break;
                case Job.Gambler:
                    Status.Attack += 2;
                    Status.Defense += 1;
                    Status.Health += 5;
                    Status.Mana += 3;
                    Luck += 2; // 도박꾼은 레벨업 시 행운 증가
                    break;
            }                
        }

        public void OpenStatus(Inventory Inventory)
        {
            int bonusAttack = Inventory.Items.Where(item => item.isEquiped).Sum(item => item.Status.Attack);
            int bonusDefense = Inventory.Items.Where(item => item.isEquiped).Sum(item => item.Status.Defense);
            int bounsLuck = Inventory.Items.Where(item => item.isEquiped).Sum(item => item.Luck);
            int TotalLuck = Luck + bounsLuck;
            int TotalAttack = Status.Attack + bonusAttack;
            int TotalDefense = Status.Defense + bonusDefense;

            Console.WriteLine("플레이어 상태창입니다. 0을 눌러 나갈 수 있습니다.");
            Console.WriteLine($"이름: {Name}");
            Console.WriteLine($"직업: {Job}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Experience: {Experience}");
            Console.WriteLine($"체력: {Health}");
            Console.WriteLine($"마나: {Mana}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"행운: {TotalLuck}+{bounsLuck}");
            Console.WriteLine("Status:");
            Console.WriteLine($"공격력: {TotalAttack}+{(bonusAttack)}");
            Console.WriteLine($"방어력: {TotalDefense}+{(bonusDefense)}");
            Console.WriteLine($"0. 나가기");

            while (true)
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
