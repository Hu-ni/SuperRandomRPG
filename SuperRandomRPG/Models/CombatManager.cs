using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Team_SRRPG.Model
{
    public class CombatManager
    {
        private Player _player;
        private List<Monster> _monsters;
        private SkillRepository _skillRepo = new SkillRepository();
        public CombatManager(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
        }
        public CombatResult StartCombat()
        {
            int originalDefense = _player.TotalDefense;
            int originalLuck = _player.TotalLuck;
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
                        bool attacked = PlayerAttack();
                        if (attacked)
                            return null;
                        else
                            DisplayCombatStatus();
                        continue;

                    case "2":
                        bool skillUsed = PlayerSkill();
                        if (skillUsed)
                            return null;
                        else
                            DisplayCombatStatus();
                        continue;
                    case "3":
                        // TODO: Implement PlayerItem()
                        Console.WriteLine("아이템은 아직 구현되지 않았습니다.");
                        Thread.Sleep(1500);
                        break;
                    case "4":
                        bool success = RunAway(_player.TotalLuck);
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

        private bool PlayerAttack()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n공격할 대상을 선택하세요");
                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (_monsters[i].Status.Health > 0)
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
                }
                Console.WriteLine("\n\n0. 취소하기");
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("공격을 취소했습니다.");
                    Thread.Sleep(1000);
                    return false;
                }

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 &&
                    choice <= _monsters.Count &&
                    _monsters[choice - 1].Status.Health > 0)
                {
                    Console.Clear();
                    Monster target = _monsters[choice - 1];
                    int damage = CalculateDamage(_player.TotalAttack, _player.TotalLuck);
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
                    return true;
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
                int penalty = 100; //일단은 50
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

                int defenseRoll = RiggedRollByLuck(_player.TotalLuck);

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
                    int baseDamage = monster.Status.Attack - _player.TotalDefense;
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
        //From here Player Skills
        private bool PlayerSkill()
        {
            List<Skill> skills = _skillRepo.GetSkillsByJob(_player.Job);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("사용할 스킬을 선택하세요:");

                for (int i = 0; i < skills.Count; i++)
                {
                    var skill = skills[i];
                    Console.WriteLine($"{i + 1}. {skill.Name} (MP: {skill.ManaCost}) - {skill.Description} - Power:{skill.Power}");
                }
                Console.WriteLine("0. 취소");
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("스킬 선택을 취소했습니다.");
                    Thread.Sleep(1000);
                    return false;
                }

                if (int.TryParse(input, out int index) && index >= 1 && index <= skills.Count)
                {
                    Skill selectedSkill = skills[index - 1];

                    if (_player.Mana < selectedSkill.ManaCost)
                    {
                        Console.WriteLine("마나가 부족합니다!");
                        Thread.Sleep(1500);
                        continue;
                    }

                    _player.Mana -= selectedSkill.ManaCost;

                    switch (selectedSkill.Type)
                    {
                        case SkillType.Attack:
                            UseAttackSkill(selectedSkill);
                            break;
                        case SkillType.Defense:
                        case SkillType.Luck:
                            UseStatBoostSkill(selectedSkill);
                            break;
                        case SkillType.Healing:
                            UseHealingSkill(selectedSkill);
                            break;
                        case SkillType.AllAttack:
                            UseAreaAttack(selectedSkill);
                            break;
                        case SkillType.WAttack:
                            UseWAttack(selectedSkill);
                            break;
                        case SkillType.GAttack:
                            UseGAttack(selectedSkill);
                            break;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    Thread.Sleep(1500);
                }
            }
            return true;
        }
        private void UseAttackSkill(Skill skill)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{skill.Name}] 사용할 대상 몬스터 선택:");

                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (_monsters[i].Status.Health > 0)
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
                }
                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= _monsters.Count &&
                    _monsters[choice - 1].Status.Health > 0)
                {
                    Monster target = _monsters[choice - 1];
                    int baseDamage = skill.Power + (_player.TotalAttack / 2);
                    Console.Clear();
                    int damage = CalculateDamage(baseDamage, _player.TotalLuck);
                    target.Status.Health = Math.Max(target.Status.Health - damage, 0);
                    Console.WriteLine($"\n{_player.Name}이(가) {skill.Name}을(를) 사용하여 {target.Name}에게 {damage}의 피해를 입혔습니다!");
                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                    Thread.Sleep(1500);
                }
            }
        }
        private void UseStatBoostSkill(Skill skill)
        {
            Console.Clear();
            int roll = RiggedRollByLuck(_player.TotalLuck);
            Random rand = new Random();

            int boostAmount = (int)(skill.Power * (0.8 + rand.NextDouble() * 0.4)); // 80~120%

            if (roll == 20)
            {
                boostAmount *= 2;
                Console.WriteLine("치명적인 강화! 효과 2배!");
            }
            else if (roll == 1)
            {
                boostAmount = (int)(boostAmount * (0.5 + rand.NextDouble() * 0.5)); // 50~100% of original
                boostAmount *= -1;
                Console.WriteLine("스킬이 역효과를 일으켰습니다!");
            }

            if (skill.Type == SkillType.Defense)
            {
                int beforeDefense = _player.TotalDefense;
                _player.TotalDefense += boostAmount;
                Console.WriteLine($"{_player.Name}의 방어력이 {Math.Abs(boostAmount)}만큼 {(boostAmount > 0 ? "증가" : "감소")}했습니다!");
                Console.WriteLine($"{beforeDefense} --> {_player.TotalDefense}");
            }
            else if (skill.Type == SkillType.Luck)
            {
                _player.TotalLuck += boostAmount;
                Console.WriteLine($"{_player.Name}의 행운이 {Math.Abs(boostAmount)}만큼 {(boostAmount > 0 ? "증가" : "감소")}했습니다!");
            }

            Thread.Sleep(3000);
        }
        private void UseHealingSkill(Skill skill)
        {
            Console.Clear();
            int roll = RiggedRollByLuck(_player.TotalLuck);
            Random rand = new Random();

            int healAmount = (int)(skill.Power * (0.8 + rand.NextDouble() * 0.4));

            if (roll == 20)
            {
                healAmount *= 2;
                Console.WriteLine("회복 효과 극대화! 회복량 2배!");
            }
            else if (roll == 1)
            {
                int damage = healAmount;
                _player.Health = Math.Max(_player.Health - damage, 0);
                Console.WriteLine("스킬이 폭주했습니다! 오히려 피해를 입었습니다!");
                Console.WriteLine($"{_player.Name}이(가) {damage}의 피해를 입었습니다!");
                Thread.Sleep(3000);
                return;
            }

            int beforeHP = _player.Health;
            _player.Health = Math.Min(_player.Health + healAmount, _player.Status.Health);

            Console.WriteLine($"{_player.Name}의 체력이 {beforeHP} → {_player.Health} 회복되었습니다!");
            Thread.Sleep(3000);
        }
        private void UseAreaAttack(Skill skill)
        {
            Console.Clear();
            Console.WriteLine($"[{skill.Name}] 스킬을 시전합니다! 모든 적에게 피해를 줍니다...\n");

            int roll = RiggedRollByLuck(_player.TotalLuck);
            Random rand = new Random();

            double multiplier;
            if (roll == 1)
            {
                Console.WriteLine("스킬이 빗나갔습니다! 아무 효과도 없습니다...");
                Thread.Sleep(3000);
                return;
            }
            else if (roll == 20)
            {
                Console.WriteLine("치명적인 광역 공격! 피해량 2배!");
                multiplier = 2.0;
            }
            else
            {
                multiplier = roll / 10.0;
            }

            int baseDamage = skill.Power + (_player.TotalAttack / 3);
            int finalDamage = (int)Math.Round(baseDamage * multiplier);

            foreach (var monster in _monsters.Where(m => m.Status.Health > 0))
            {
                int damageDealt = Math.Clamp(finalDamage - monster.Status.Defense, 0, 999);
                int beforeHP = monster.Status.Health;
                monster.Status.Health = Math.Max(monster.Status.Health - damageDealt, 0);

                Console.WriteLine($"{monster.Name}에게 {damageDealt}의 피해를 입혔습니다! (HP: {beforeHP} → {monster.Status.Health})");
            }
            Thread.Sleep(3500);
        }
        private void UseWAttack(Skill skill)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{skill.Name}] 사용할 대상 몬스터 선택:");

                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (_monsters[i].Status.Health > 0)
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
                }

                Console.Write(">> ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= _monsters.Count &&
                    _monsters[choice - 1].Status.Health > 0)
                {
                    Console.Clear();
                    Monster target = _monsters[choice - 1];

                    int roll = RiggedRollByLuck(_player.TotalLuck);
                    double multiplier;

                    if (roll == 1)
                    {
                        Console.WriteLine("스킬이 빗나갔습니다! 아무 효과도 없습니다...");
                        Thread.Sleep(3000);
                        return;
                    }
                    else if (roll == 20)
                    {
                        Console.WriteLine("방어를 활용한 치명타! 피해량 2배!");
                        multiplier = 2.0;
                    }
                    else
                    {
                        multiplier = roll / 10.0;
                    }

                    int baseDamage = skill.Power + (_player.TotalDefense / 2);
                    int totalDamage = (int)Math.Round(baseDamage * multiplier);
                    int finalDamage = Math.Clamp(totalDamage - target.Status.Defense, 0, 999);

                    int beforeHP = target.Status.Health;
                    target.Status.Health = Math.Max(target.Status.Health - finalDamage, 0);

                    Console.WriteLine($"{_player.Name}이(가) {skill.Name}을(를) 사용하여 {target.Name}에게 {finalDamage}의 피해를 입혔습니다!");
                    Console.WriteLine($"{target.Name}: HP {beforeHP} → {target.Status.Health}");

                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                    Thread.Sleep(1500);
                }
            }
        }
        private void UseGAttack(Skill skill)
        {
            Monster target = null!;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{skill.Name}]은(는) 플레이어의 골드를 투자해 강력한 공격을 하는 기술입니다!");
                Console.WriteLine("공격할 대상을 선택하세요:");
                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (_monsters[i].Status.Health > 0)
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} (HP: {_monsters[i].Status.Health})");
                }

                Console.Write(">> ");
                string? targetInput = Console.ReadLine();

                if (int.TryParse(targetInput, out int choice) &&
                    choice >= 1 && choice <= _monsters.Count &&
                    _monsters[choice - 1].Status.Health > 0)
                {
                    target = _monsters[choice - 1];
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                    Thread.Sleep(1500);
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{skill.Name}]을(를) 사용하여 {target.Name}을(를) 공격합니다!");
                Console.WriteLine($"현재 보유 골드: {_player.Gold}");
                Console.Write("얼마의 골드를 투자하시겠습니까? >> ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int goldInvestment) && goldInvestment > 0 && goldInvestment <= _player.Gold)
                {
                    Console.Clear();
                    Console.WriteLine($"골드 {goldInvestment}을(를) 투자하여 {skill.Name} 시전 중...\n");

                    int roll = RiggedRollByLuck(_player.TotalLuck);
                    double multiplier;

                    if (roll == 1)
                    {
                        Console.WriteLine("대실패! 공격이 빗나가고 골드만 날렸습니다...");
                        _player.Gold -= goldInvestment;
                        Thread.Sleep(3000);
                        return;
                    }
                    else if (roll == 20)
                    {
                        Console.WriteLine("대성공! 피해량이 2배로 증가합니다!");
                        multiplier = 2.0;
                    }
                    else
                    {
                        multiplier = roll / 10.0;
                    }

                    int baseDamage = skill.Power * goldInvestment;
                    int finalDamage = (int)Math.Round(baseDamage * multiplier);
                    int beforeHP = target.Status.Health;

                    int damageDealt = Math.Clamp(finalDamage - target.Status.Defense, 0, 999);
                    target.Status.Health = Math.Max(target.Status.Health - damageDealt, 0);
                    _player.Gold -= goldInvestment;

                    Console.WriteLine($"{_player.Name}이(가) {skill.Name}으로 {target.Name}에게 {damageDealt}의 피해를 입혔습니다!");
                    Console.WriteLine($"{target.Name}: HP {beforeHP} → {target.Status.Health}");
                    Console.WriteLine($"골드 {goldInvestment}이(가) 차감되었습니다. 현재 골드: {_player.Gold}");
                    Thread.Sleep(3000);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 골드 입력입니다.");
                    Thread.Sleep(1500);
                }
            }
        }


    }
}
