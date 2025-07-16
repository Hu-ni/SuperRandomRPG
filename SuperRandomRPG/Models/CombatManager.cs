using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Team_SRRPG.Model
{
    public class CombatManager
    {
        private readonly Player _player;
        private readonly List<Monster> _monsters;
        private int _currentHealth; //Temp
        public CombatManager(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
            _currentHealth = player.Health; //Temp
        }
        public CombatResult StartCombat()
        {
            while (_monsters.Any(m => m.Status.Health > 0) && _currentHealth > 0)
            {
                DisplayCombatStatus();
                CombatResult? turnResult = PlayerTurn();
                if (turnResult == CombatResult.Escaped)
                {
                    return CombatResult.Escaped;
                }

                if (_monsters.All(m => m.Status.Health <= 0))
                {
                    Console.Clear();
                    Console.WriteLine("전투에 승리하셨습니다!");
                    int totalExp = _monsters.Sum(m => m.Reward.Exp);
                    int totalGold = _monsters.Sum(m => m.Reward.Money);
                    _player.Experience += totalExp;
                    _player.Gold += totalGold;
                    Console.WriteLine($"\n획득 보상: EXP +{totalExp}, Gold +{totalGold}");
                    Thread.Sleep(2000);
                    return CombatResult.Victory;
                }

                MonsterTurn();
            }
            return CombatResult.Defeat;
        }
        private void DisplayCombatStatus()
        {
            Console.Clear();
            Console.WriteLine("========== 전투 ==========");
            Console.WriteLine($"{_player.Name} HP: {_currentHealth}/{_player.Health}\n");

            Console.WriteLine(">> 적 목록:");
            for (int i = 0; i < _monsters.Count; i++)
            {
                var m = _monsters[i];
                string status = m.Status.Health > 0 ? $"HP: {m.Status.Health}" : "(쓰러짐)";
                Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name} - {status}");
            }
        }
        private CombatResult? PlayerTurn()
        {
            Console.WriteLine("\n1. 공격하기");
            Console.WriteLine("2. 스킬 사용");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine("4. 도망가기");
            Console.Write(">> ");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    PlayerAttack();
                    break;
                case "2":
                    // TODO PlayerSkill();
                    break;
                case "3":
                    // TODO PlayerItem();
                    break;
                case "4":
                    bool success = RunAway(_player.Luck);
                    if (success)
                        return CombatResult.Escaped;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
            return null; //도망 실패
        }

        private void PlayerAttack()
        {
            Console.Clear();
            Console.WriteLine("\n공격할 대상을 선택하세요:");
            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].Status.Health > 0)
                    Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
            }
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _monsters.Count &&
                _monsters[choice - 1].Status.Health > 0)
            {
                Monster target = _monsters[choice - 1];
                int damage = CalculateDamage(_player.Status.Attack, _player.Luck);
                int totaldamge = Math.Clamp(damage - target.Status.Defense, 1, 999);
                target.Status.Health -= totaldamge;

                if (target.Status.Health < 0) target.Status.Health = 0;

                Console.WriteLine($"\n{_player.Name}이(가) {target.Name}에게 {totaldamge}의 피해를 입혔습니다!");
                Thread.Sleep(3000);
            }
        }
        private bool RunAway(int luck)
        {
            double averageMonsterLevel = _monsters
                .Where(m => m.Status.Health > 0)
                .Average(m => m.Level);
            int levelDiff = _player.Level - (int)Math.Round(averageMonsterLevel);
            double chanceToRun = 0.5 + (levelDiff * 0.1);
            chanceToRun = Math.Clamp(chanceToRun, 0.1, 0.9); // 10% ~ 90%

            Console.WriteLine($"플레이어 레벨: {_player.Level}, 몬스터 평균 레벨: {averageMonsterLevel:F1}");
            Console.WriteLine($"도망 확률: {chanceToRun * 100:F0}%");
            int roll = RiggedRollByLuck(luck);
            Thread.Sleep(3000);
            int successThreshold = (int)Math.Round(20 * (1 - chanceToRun)) + 1;

            if (roll >= successThreshold)
            {
                int penalty = 50; //일단은 50
                _player.Gold = Math.Max(0, _player.Gold - penalty);
                Console.WriteLine($"도망에 성공했습니다! Gold {penalty}를 잃었습니다.");
                Thread.Sleep(2000);
                return true;
            }
            else
            {
                Console.WriteLine("도망에 실패했습니다!");
                Thread.Sleep(2000);
                return false;
            }
        }


        private void MonsterTurn()
        {
            foreach (var monster in _monsters.Where(m => m.Status.Health > 0))
            {
                // TODO: implement attacks
            }
        }

        private int RiggedRollByLuck(int luck)
        {
            Random rnd = new Random();
            int numRolls = 1 + (luck / 10);
            int maxRoll = 0;

            Console.Write("Rolls: ");
            for (int i = 0; i < numRolls; i++)
            {
                int roll = rnd.Next(1, 21);
                Console.Write(roll + (i < numRolls - 1 ? ", " : "\n"));
                if (roll > maxRoll)
                    maxRoll = roll;
            }
            return maxRoll;
        }

        private int CalculateDamage(int baseDamage, int luck)
        {
            int roll = RiggedRollByLuck(luck);
            double multiplier;

            if (roll == 1)
            {
                Console.WriteLine("공격 실패했습니다!");
                Thread.Sleep(1000);
                return 0;
            }
            else if (roll == 20)
            {
                Console.WriteLine("치명타!");
                multiplier = 2.0;
            }
            else
            {
                multiplier = roll / 10.0;
            }
            double finalDamage = baseDamage * multiplier;
            return (int)Math.Round(finalDamage);
        }
    }
}
