using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Team_SRRPG.Model
{
    public class DungeonManager
    {
        private Player _player;
        private Inventory _inventory;
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
                    Description  = "너무나도 익숙한 곳...",
                    Difficult = 1,
                    MinMonsterLevel = 1,
                    MaxMonsterLevel = 5,
                    Monsters = new List<Monster>
                    {
                        new Monster
                        {
                            Id = 1, Name = "근거리 미니언", Level = 1,
                            BaseHealth = 12, HealthPerLvl = 5,
                            BaseAttack = 6, AttackPerLvl = 2,
                            BaseDefense = 4, DefensePerLvl = 1,
                            Reward = new Reward{ Exp = 10, Money = 15 }
                        },
                        new Monster
                        {
                            Id = 2, Name = "원거리 미니언", Level = 1,
                            BaseHealth = 10, HealthPerLvl = 4,
                            BaseAttack = 8, AttackPerLvl = 3,
                            BaseDefense = 2, DefensePerLvl = 0,
                            Reward = new Reward { Exp = 10, Money = 15 }
                        },
                        new Monster
                        {
                            Id = 3, Name = "탱크 미니언", Level = 1,
                            BaseHealth = 15, HealthPerLvl = 6,
                            BaseAttack = 7, AttackPerLvl = 2,
                            BaseDefense = 6, DefensePerLvl = 2,
                            Reward = new Reward {Exp = 15, Money = 20 }
                        }
                    },
                    Reward = new Reward { Exp = 50, Money = 100 }
                },
                new Dungeon
                {
                    Id = 2,
                    Name = "카지노 (라스베가스)",
                    Description = "돈! 돈!! 돈!!!",
                    Difficult = 2,
                    MinMonsterLevel = 5,
                    MaxMonsterLevel = 15,
                    Monsters = new List<Monster>
                    {
                        new Monster
                        {
                            Id = 4, Name = "킹 오브 하트", Level = 1,
                            BaseHealth = 20, HealthPerLvl = 6,
                            BaseAttack = 6, AttackPerLvl = 2,
                            BaseDefense = 3, DefensePerLvl = 1,
                            Reward = new Reward{ Exp = 20, Money = 50 }
                        },
                        new Monster
                        {
                            Id = 5, Name = "에이스", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 3,
                            BaseAttack = 10, AttackPerLvl = 2,
                            BaseDefense = 2, DefensePerLvl = 1,
                            Reward = new Reward{ Exp = 20, Money = 25 }
                        },
                        new Monster
                        {
                            Id = 6, Name = "조커", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 5,
                            BaseAttack = 11, AttackPerLvl = 3,
                            BaseDefense = 5, DefensePerLvl = 2,
                            Reward = new Reward{ Exp = 50, Money = 20 }
                        }
                    }
                },
                new Dungeon
                {
                    Id = 3,
                    Name = "내일배움 캠프 (수직적 구조)",
                    Description = "와 프로젝트 만든 사람들이다",
                    Difficult = 3,
                    MinMonsterLevel = 10,
                    MaxMonsterLevel = 30,
                    Monsters = new List<Monster>
                    {
                        new Monster
                        {
                            Id = 4, Name = "박훈", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 10,
                            BaseAttack = 15, AttackPerLvl = 3,
                            BaseDefense = 15, DefensePerLvl = 3,
                            Reward = new Reward{ Exp = 100, Money = 200 }
                        },
                        new Monster
                        {
                            Id = 5, Name = "김영수", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 10,
                            BaseAttack = 15, AttackPerLvl = 3,
                            BaseDefense = 15, DefensePerLvl = 3,
                            Reward = new Reward{ Exp = 100, Money = 200 }
                        },
                        new Monster
                        {
                            Id = 6, Name = "성준우", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 10,
                            BaseAttack = 15, AttackPerLvl = 3,
                            BaseDefense = 15, DefensePerLvl = 3,
                            Reward = new Reward{ Exp = 100, Money = 200 }
                        },
                        new Monster
                        {
                            Id = 6, Name = "김상균", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 10,
                            BaseAttack = 15, AttackPerLvl = 3,
                            BaseDefense = 15, DefensePerLvl = 3,
                            Reward = new Reward{ Exp = 100, Money = 200 }
                        },
                        new Monster
                        {
                            Id = 6, Name = "임성준", Level = 1,
                            BaseHealth = 30, HealthPerLvl = 10,
                            BaseAttack = 15, AttackPerLvl = 3,
                            BaseDefense = 15, DefensePerLvl = 3,
                            Reward = new Reward{ Exp = 100, Money = 200 }
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
        
        private void DrawRoomUI(string dungeonName, int currentRoom, int maxRooms)
        {
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
            Console.WriteLine($"[{dungeonName}] {currentRoom + 1}/{maxRooms} 방 탐색 중...");
            Console.WriteLine("1. 다음 방으로 이동\n2. 상태보기");
        }

        private void EnterDungeon(Dungeon dungeon)
        {
            int currentRoom = 0;
            int maxRooms = 3;

            while (currentRoom < maxRooms)
            {
                Console.Clear();
                Console.Clear();
                DrawRoomUI(dungeon.Name, currentRoom, maxRooms);
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
                        const string SaveFilePath = "Player.xml";

                        if (File.Exists(SaveFilePath))
                        {
                            File.Delete(SaveFilePath);
                            Console.WriteLine("세이브 파일이 삭제되었습니다.");
                        }
                        else
                        {
                            Console.WriteLine("세이브 파일이 존재하지 않아 삭제할 수 없습니다.");
                        }

                        Thread.Sleep(1500);
                        Console.WriteLine("게임을 종료합니다...");
                        Thread.Sleep(2000);
                        Environment.Exit(0); // 💥 End game
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
                    _player.OpenStatus();
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
            CheckLevelUp();
            Thread.Sleep(2000);
        }
        private void CheckLevelUp()
        {
            if (_player.Experience >= _player.Level * 50)
            {
                _player.LevelUp();
            }
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
