using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Team_SRRPG.Model
{
    public class DungeonManager
    {
        private Player _player;
        private List<Dungeon> _dungeons;
        private static Random _rng = new();

        public DungeonManager(Player player)
        {
            _player = player;
            _dungeons = CreateDefaultDungeons();
        }

        private List<Dungeon> CreateDefaultDungeons()
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
                        new Monster { Id = 2, Name = "PlaceHolder2", Status = new Status { Health = 2, Attack = 2 , Defense = 2 }, Reward = new Reward { Exp = 3 , Money = 3 }},
                        new Monster { Id = 3, Name = "PlaceHolder3", Status = new Status { Health = 2, Attack = 2, Defense = 3 }, Reward = new Reward { Exp = 3 , Money = 3 }},
                    },
                    Reward = new Reward { Exp = 50, Money = 100 }
                }
            };
        }

        public void ShowDungeonSelectionScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 던전 선택 ===");
                for (int i = 0; i < _dungeons.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_dungeons[i].Name} - {_dungeons[i].Description} (난이도: {_dungeons[i].Difficult})");
                }
                Console.WriteLine("0. 마을로 돌아가기");
                Console.Write(">> ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int selection))
                {
                    if (selection == 0) return;

                    if (selection >= 1 && selection <= _dungeons.Count)
                    {
                        EnterDungeon(_dungeons[selection - 1]);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        private void EnterDungeon(Dungeon dungeon)
        {
            int currentRoom = 0;
            int maxRooms = 3;

            while (currentRoom < maxRooms)
            {
                Console.Clear();
                Console.WriteLine($"[{dungeon.Name}] {currentRoom + 1}/{maxRooms} 방 탐색 중...");
                Console.WriteLine("1. 다음 방으로 이동\n2. 아이템 사용하기\n3. 상태보기");
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("\n...다음 방으로 이동 중...");
                    Thread.Sleep(1000);

                    var monsters = GetRandomMonsters(dungeon);
                    var survived = Combat.CombatPhase(monsters, _player);
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
                    // _player.ShowItems();
                    Console.ReadLine();
                }
                else if (input == "3")
                {
                    Console.Clear();
                    // _player.ShowStatus();
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }

            Console.Clear();
            Console.WriteLine($"{dungeon.Name} 클리어!");
            Console.WriteLine($"보상: Gold +{dungeon.Reward.Money}, EXP +{dungeon.Reward.Exp}");
            _player.Gold += dungeon.Reward.Money;
            _player.Experience += dungeon.Reward.Exp;
            // _player.CheckLevelUp();
            Thread.Sleep(2000);
        }

        private List<Monster> GetRandomMonsters(Dungeon dungeon)
        {
            int count = _rng.Next(1, 5); // 1~4 monsters
            var encounteredMonsters = new List<Monster>();

            for (int i = 0; i < count; i++)
            {
                var selected = dungeon.Monsters[_rng.Next(dungeon.Monsters.Count)];
                encounteredMonsters.Add(selected.Clone());
            }

            Console.Clear();
            Console.WriteLine($">> {count}마리의 몬스터를 만났습니다!\n");

            foreach (var m in encounteredMonsters)
            {
                Console.WriteLine($"이름: {m.Name}");
                Console.WriteLine($"체력: {m.Status.Health} / 공격력: {m.Status.Attack} / 방어력: {m.Status.Defense}");
                Console.WriteLine();
            }
            Thread.Sleep(5000);
            return encounteredMonsters;
        }
    }
}
