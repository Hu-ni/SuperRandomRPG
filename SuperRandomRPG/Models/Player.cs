using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        public Inventory Inventory { get; set; } // 플레이어 인벤토리
        public int TotalAttack => Status.Attack + Inventory.Items.Where(x => x.isEquiped).Sum(x => x.Status.Attack);
        public int TotalDefense => Status.Defense + Inventory.Items.Where(x => x.isEquiped).Sum(x => x.Status.Defense) + TempDefenseBoost;
        public int TotalLuck => Luck + Inventory.Items.Where(x => x.isEquiped).Sum(x => x.Luck) + TempLuckBoost;
        public int TempDefenseBoost { get; set; } = 0;
        public int TempLuckBoost { get; set; } = 0;



        public Player(string name, Job job, int level = 1, int exp = 0, int gold = 1000)
        {
            Name = name;
            Job = job;
            Level = level;
            Experience = exp;
            Gold = gold;
            Inventory = new Inventory();

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
            Console.Clear();
            Console.WriteLine("========== 레벨 업! ==========");
            Console.WriteLine($"{Name} 님이 레벨업했습니다! 현재 레벨: {Level}");
            int HpUp = 0 , MpUp = 0, AtkUp = 0, DefUp = 0, LuckUp = 0;
            switch (Job)
            {
                case Job.Warrior:
                    HpUp = 20; MpUp = 10; AtkUp = 5; DefUp = 3; LuckUp = 0;
                    break;
                case Job.Mage:
                    HpUp = 15; MpUp = 20; AtkUp = 4; DefUp = 2; LuckUp = 0;
                    break;
                case Job.Archer:
                    HpUp = 18; MpUp = 15; AtkUp = 3; DefUp = 2; LuckUp = 0;
                    break;
                case Job.Gambler:
                    HpUp = 10; MpUp = 5; AtkUp = 2; DefUp = 1; LuckUp = 2;
                    break;
            }
            Status.Health += HpUp;
            Status.Mana += MpUp;
            Status.Attack += AtkUp;
            Status.Defense += DefUp;
            Luck += LuckUp;
            Health = Status.Health; // 레벨업 시 체력 업데이트
            Mana = Status.Mana; // 레벨업 시 마나 업데이트
            Console.WriteLine($"체력 +{HpUp}, 마나 +{MpUp}, 공격력 +{AtkUp}, 방어력 +{DefUp}, 행운 +{LuckUp}");
            Thread.Sleep(3000);
        }

        public void OpenStatus()
        {
            Console.Clear();
            int bonusAttack = TotalAttack - Status.Attack;
            int bonusDefense = TotalDefense - Status.Defense;
            int bonusluck = TotalLuck - Luck;

            Console.WriteLine("플레이어 상태창입니다. 0을 눌러 나갈 수 있습니다.");
            Console.WriteLine($"이름: {Name}");
            Console.WriteLine($"직업: {Job}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Experience: {Experience}");
            Console.WriteLine($"체력: {Health}");
            Console.WriteLine($"마나: {Mana}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"행운: {TotalLuck}+{bonusluck}");
            Console.WriteLine("Status:");
            Console.WriteLine($"공격력: {TotalAttack}+{bonusAttack}");
            Console.WriteLine($"방어력: {TotalDefense}+{bonusDefense}");
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
