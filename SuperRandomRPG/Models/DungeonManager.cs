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
                    MinMonsterLevel = 1,
                    MaxMonsterLevel = 5,
                    Monsters = new List<Monster>
                    {
                        new Monster
                        {
                            Id = 1, Name = "근거리 미니언", Level = 1,
                            BaseHealth = 3, HealthPerLvl = 2,
                            BaseAttack = 1, AttackPerLvl = 1,
                            BaseDefense = 1, DefensePerLvl = 0
                        },
                        new Monster
                        {
                            Id = 2, Name = "원거리 미니언", Level = 1,
                            BaseHealth = 3, HealthPerLvl = 1,
                            BaseAttack = 2, AttackPerLvl = 2,
                            BaseDefense = 0, DefensePerLvl = 0
                        },
                        new Monster
                        {
                            Id = 3, Name = "탱크 미니언", Level = 1,
                            BaseHealth = 5, HealthPerLvl = 3,
                            BaseAttack = 3, AttackPerLvl = 2,
                            BaseDefense = 2, DefensePerLvl = 1
                        }
                    },
                    Reward = new Reward { Exp = 50, Money = 100 }
                },
                new Dungeon
                {
                    Id = 2,
                    Name = "Testing Dungeon 2",
                    Description = "Place Holder",
                    Difficult = 2,
                    MinMonsterLevel = 5,
                    MaxMonsterLevel = 20,
                    Monsters = new List<Monster>
                    {
                        new Monster
                        {
                            Id = 4, Name = "Slime", Level = 1,
                            BaseHealth = 5, HealthPerLvl = 3,
                            BaseAttack = 1, AttackPerLvl = 1,
                            BaseDefense = 0, DefensePerLvl = 1
                        },
                        new Monster
                        {
                            Id = 5, Name = "Slime", Level = 1,
                            BaseHealth = 5, HealthPerLvl = 3,
                            BaseAttack = 1, AttackPerLvl = 1,
                            BaseDefense = 0, DefensePerLvl = 1
                        },
                        new Monster
                        {
                            Id = 6, Name = "Slime", Level = 1,
                            BaseHealth = 5, HealthPerLvl = 3,
                            BaseAttack = 1, AttackPerLvl = 1,
                            BaseDefense = 0, DefensePerLvl = 1
                        }
                    }
                }
            };
        }

        public void ShowDungeonSelectionScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"
                 _______  __   __  ___   _  ________ _______  _______  ___   _ 
                |  _    ||  | |  ||   |_| ||    ___||    ___||   _   ||   |_| |
                | | |   ||  |_|  ||       ||   | __ |   |___ |  | |  ||       |
                | |_|   ||       ||  _    ||   ||  ||    ___||  |_|  ||  _    |
                |       ||       || | |   ||   |_| ||   |___ |       || | |   |
                |______| |_______||_|  |__||_______||_______||_______||_|  |__|
                            ⠀⠀⠀⠀⠀⠀⠀⢠⣴⣾⣷⠈⣿⣿⣿⣿⣿⡟⢀⣿⣶⣤⠀⠀⠀⠀⠀⠀⠀⠀
                            ⠀⠀⠀⠀⢠⣾⣷⡄⠻⣿⣿⣧⠘⣿⣿⣿⡿⠀⣾⣿⣿⠃⣰⣿⣶⣄⠀⠀⠀⠀
                            ⠀⠀⠀⣴⣿⣿⣿⡿⠆⠉⠉⠁⠀⠈⠉⠉⠁⠀⠙⠛⠃⢰⣿⣿⣿⣿⣷⡀⠀⠀
                            ⠀⠀⣼⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  ⠉⢿⣿⣿⣿⣷⠀⠀
                            ⠀⠘⠛⠛⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  ⠻⠛⠛⠛⠃⠀
                            ⠀⢸⣿⣿⣿⡇⠀⠀⣀⣰⡄⠀⠀⠀⠀⠀⠀⠀⠀  ⢠⣶⣠⠀⠀ ⢰⣾⣿⣿⡇⠀
                            ⠀⢸⡿⠿⠿⠇⠀⢟⠉⠁⣳⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⠈⠈⡿⠀ ⢸⣿⣿⣿⡇⠀
                            ⠀⣤⣤⣴⣶⡆⠀⢠⣀⡀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠈ ⢀⣤⡀⠀ ⠘⠿⠿⠿⠇⠀
                            ⠀⣿⣿⣿⣿⣷⠀⢸⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⢿⡇⠀  ⣶⣶⣶⣶⣶⠀
                            ⠀⣿⣿⣿⣿⣿⠀⠚⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠘⠃⠀ ⣿⣿⣿⣿⣿⠀
                            ⠀⠛⠛⠛⠛⢛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀   ⠙⠛⠛⠋⣁⠀
                            ⠀⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀   ⣿⣿⣿⣿⣿⠀
                ");
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
                Console.Clear();
                Console.WriteLine(" __________   __________   __________");
                Console.WriteLine("|          | |          | |          |");

                for (int i = 0; i < maxRooms; i++)
                {
                    if (i == currentRoom)
                        Console.Write("|    O     |=");
                    else
                        Console.Write("|          |=");
                }
                Console.WriteLine();
                Console.WriteLine("|__________|=|__________|=|__________|\n\n");
                Console.WriteLine($"[{dungeon.Name}] {currentRoom + 1}/{maxRooms} 방 탐색 중...");
                Console.WriteLine("1. 다음 방으로 이동\n2. 아이템 사용하기\n3. 상태보기");
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("\n...다음 방으로 이동 중...");
                    Thread.Sleep(1000);

                    var monsters = GetRandomMonsters(dungeon);
                    var combatManager = new CombatManager(_player, monsters);
                    CombatResult result = combatManager.StartCombat();

                    if (result == CombatResult.Defeat)
                    {
                        Console.WriteLine("전투에서 패배했습니다... 당신은 죽었습니다.");
                        Thread.Sleep(2000);
                        return; 
                    }
                    else if (result == CombatResult.Escaped)
                    {
                        Console.WriteLine("당신은 던전 입구로 도망쳤습니다.");
                        Thread.Sleep(2000);
                        return;
                    }
                    currentRoom++;
                }
                else if (input == "2")
                {
                    Console.Clear();
                    // _player.ShowItems(); Not yet Done
                    Console.ReadLine();
                }
                else if (input == "3")
                {
                    Console.Clear();
                    //_player.OpenStatus();
                    Console.Clear();
                    Console.WriteLine($"[{dungeon.Name}] {currentRoom + 1}/{maxRooms} 방 탐색 중...");
                    Console.WriteLine("1. 다음 방으로 이동\n2. 아이템 사용하기\n3. 상태보기");
                }
                else
                {
                    Console.Clear();
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
                var clones = selected.Clone();

                int level = _rng.Next(dungeon.MinMonsterLevel, dungeon.MaxMonsterLevel + 1);
                clones.Level = level;
                clones.Status = new Status
                {
                    Health = clones.BaseHealth + level * clones.HealthPerLvl,
                    Attack = clones.BaseAttack + level * clones.AttackPerLvl,
                    Defense = clones.BaseDefense + level * clones.DefensePerLvl
                };
                clones.Reward = new Reward
                {
                    Exp = 5 + level * 3,
                    Money = 10 + level * 5
                };

                encounteredMonsters.Add(clones);
            }

            Console.Clear();
            Console.WriteLine($">> {count}마리의 몬스터를 만났습니다!\n");

            foreach (var m in encounteredMonsters)
            {
                Console.WriteLine($"이름: Lv.{m.Level} {m.Name}");
                Console.WriteLine($"체력: {m.Status.Health} / 공격력: {m.Status.Attack} / 방어력: {m.Status.Defense}");
                Console.WriteLine();
            }
            Thread.Sleep(5000);
            return encounteredMonsters;
        }
    }
}
