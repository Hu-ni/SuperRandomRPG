using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SRRPG.Model
{
    // 데이터 저장될 그릇
    // 최소 단위 *
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficult { get; set; }

        public List<Monster> Monsters { get; set; }

        // 던전 클리어 보상
        public Reward Reward { get; set; }
        private static Random _rng = new Random();

        public static List<Dungeon> CreateDefaultDungeons()
        {
            return new List<Dungeon>
            {
                new Dungeon
                {
                    Id = 1,
                    Name = "칼바람 나락",
                    Description  = "Placeholder",
                    Difficult = 1,
                    Monsters = new List<Monster>
                    {
                        new Monster { Id = 1, Name = "PlaceHolder", Status = new Status { Health = 2, Attack = 2, Defense = 2 }, Reward = new Reward { Exp = 3 , Money = 30}},
                        new Monster { Id = 2, Name = "PlaceHolder2", Status = new Status { Health = 2, Attack = 2 , Defense =2 },Reward = new Reward {Exp = 3 , Money = 3 }},
                        new Monster { Id = 3, Name = "PlaceHolder3", Status = new Status { Health = 2, Attack = 2, Defense =3 }, Reward = new Reward {Exp = 3 , Money = 3 } },
                    },
                    Reward = new Reward {Exp = 50, Money = 100 }
                }
            };
        }

        public static void DungeonSelectionScreen(Player player)
        {
            var dungeons = CreateDefaultDungeons();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 던전 선택 ===");
                for (int i = 0; i < dungeons.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dungeons[i].Name} - {dungeons[i].Description} (난이도: {dungeons[i].Difficult})");
                }
                Console.WriteLine("0. 마을로 돌아가기");
                Console.Write(">> ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int selection))
                {
                    if (selection == 0) return;

                    if (selection >= 1 && selection <= dungeons.Count)
                    {
                        EnterDungeon(dungeons[selection - 1], player);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void EnterDungeon(Dungeon dungeons, Player player)
        {
            int currentRoom = 0;
            int maxRooms = 3;
            while (currentRoom < maxRooms)
            {
                Console.Clear();
                Console.WriteLine($"[{dungeons.Name}] {currentRoom + 1}/{maxRooms} 방 탐색 중...");
                Console.WriteLine("1. 다음 방으로 이동\n2. 아이템 사용하기\n3. 상태보기");
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("\n...다음 방으로 이동 중...");
                    Thread.Sleep(1000);

                    var monsters = GetRandomMonsters(dungeons);
                    var survived = Combat.CombatPhase(monsters, player);
                    if (!survived)
                    {
                        Console.WriteLine("전투에서 패배했습니다...");
                        Thread.Sleep(2000);
                        return;
                    }

                    currentRoom++;
                }
                else if (input == "2")
                {
                    Console.Clear();
                    //player.ShowStatus();
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
            Console.Clear();
            Console.WriteLine($"{dungeons.Name} 클리어!");
            Console.WriteLine($"보상: Gold +{dungeons.Reward.Money}, EXP +{dungeons.Reward.Exp}");
            player.Gold += dungeons.Reward.Money;
            player.Experience += dungeons.Reward.Exp;
            //player.CheckLevelUp();
            Thread.Sleep(2000);
        }
        private static List<Monster> GetRandomMonsters(Dungeon dungeon)
        {
            int count = _rng.Next(1, 5); //1~4 Monsters
            var encmosnters = new List<Monster>();

            for (int i = 0; i < count; i++)
            {
                var selected = dungeon.Monsters[_rng.Next(dungeon.Monsters.Count)];
                encmosnters.Add(selected);
            }
            Console.Clear();
            Console.WriteLine($">> {count}마리의 몬스터를 만났습니다!\n");

            foreach (var m in encmosnters)
            {
                Console.WriteLine($"이름: {m.Name}");
                Console.WriteLine($"체력: {m.Status.Health} / 공격력: {m.Status.Attack} / 방어력: {m.Status.Defense}");
                Console.WriteLine();
            }
            Thread.Sleep(5000);
            return encmosnters;
        }
    }
}
