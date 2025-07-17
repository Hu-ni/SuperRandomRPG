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
        public CombatManager(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
        }
        public CombatResult StartCombat()
        {
            while (_monsters.Any(m => m.Status.Health > 0) && _player.Health > 0)
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
            Console.WriteLine($"{_player.Name} HP: {_player.Health}/{_player.Status.Health}\n");

            Console.WriteLine(">> 적 목록:");
            for (int i = 0; i < _monsters.Count; i++)
            {
                var m = _monsters[i];
                if (m.Status.Health > 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name} - HP: {m.Status.Health}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name} - (쓰러짐)");
                }
            }
            Console.ResetColor();
        }
        private CombatResult? PlayerTurn()
        {
            while (true)
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
                        return null;
                    case "2":
                        // TODO: Implement PlayerSkill()
                        Console.WriteLine("스킬은 아직 구현되지 않았습니다.");
                        Thread.Sleep(1500);
                        break;
                    case "3":
                        // TODO: Implement PlayerItem()
                        Console.WriteLine("아이템은 아직 구현되지 않았습니다.");
                        Thread.Sleep(1500);
                        break;
                    case "4":
                        bool success = RunAway(_player.Luck);
                        return success ? CombatResult.Escaped : null;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                        Thread.Sleep(1500);
                        break;
                }
                DisplayCombatStatus();
            }
        }

        private void PlayerAttack()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n공격할 대상을 선택하세요:");
                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (_monsters[i].Status.Health > 0)
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
                }
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 &&
                    choice <= _monsters.Count &&
                    _monsters[choice - 1].Status.Health > 0)
                {
                    Console.Clear();
                    Monster target = _monsters[choice - 1];
                    int damage = CalculateDamage(_player.Status.Attack, _player.Luck);
                    int totalDamage = Math.Clamp(damage - target.Status.Defense, 0, 999);

                    if (totalDamage <= 0)
                    {
                        Console.WriteLine($"\n{_player.Name}의 공격이 {target.Name}에게 닿지 않았습니다!");
                    }
                    else
                    {
                        int beforeHP = target.Status.Health;
                        target.Status.Health -= totalDamage;
                        target.Status.Health = Math.Max(target.Status.Health, 0);

                        Console.WriteLine($"\n{_player.Name}이(가) {target.Name}에게 {totalDamage}의 피해를 입혔습니다!");
                        Console.WriteLine($"{target.Name}: HP {beforeHP} → {target.Status.Health}");
                    }
                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 선택입니다. 다시 입력하세요.");
                    Thread.Sleep(1500);
                }
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
                Console.Clear();
                Console.WriteLine($"{monster.Name}이(가) 공격합니다!\n");

                int defenseRoll = RiggedRollByLuck(_player.Luck);

                if (defenseRoll == 20)
                {
                    Console.WriteLine($"{_player.Name}이(가) {monster.Name}의 공격을 완벽히 피했습니다!");
                }
                else if (defenseRoll == 1)
                {
                    Console.WriteLine($"{monster.Name}의 치명타 공격! {monster.Status.Attack}피해를 받았습니다");
                    _player.Health -= monster.Status.Attack;
                }
                else
                {
                    int baseDamage = monster.Status.Attack - _player.Status.Defense;
                    baseDamage = Math.Clamp(baseDamage, 1, 999);
                    double damageMultiplier = 1.0 - (defenseRoll / 25.0);
                    damageMultiplier = Math.Clamp(damageMultiplier, 0.2, 1.0);
                    int finalDamage = Math.Clamp((int)Math.Round(baseDamage * damageMultiplier), 1, 999);
                    _player.Health -= finalDamage;
                    if (_player.Health < 0) _player.Health = 0;
                    Console.WriteLine($"방어 굴림: {defenseRoll}\n");
                    Console.WriteLine($"{monster.Name}이(가) {_player.Name}에게 {finalDamage}의 피해를 입혔습니다!");
                    Console.WriteLine($"{_player.Name}의 현재 HP: {_player.Health}/{_player.Status.Health}");
                }
                Thread.Sleep(3000);
                if (_player.Health <= 0)
                {
                    Console.WriteLine($"{_player.Name}이(가) 쓰러졌습니다...");
                    Thread.Sleep(2000);
                    break;
                }
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
            Console.WriteLine($"Result : {roll}");
            double finalDamage = baseDamage * multiplier;
            return (int)Math.Round(finalDamage);
        }
    }
}
