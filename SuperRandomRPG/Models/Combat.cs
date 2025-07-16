using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SRRPG.Model
{
    public class Combat
    {
        public static bool CombatPhase(List<Monster> monsters, Player player)
        {
            int currentHealth = player.Health; // Placeholder

            while (monsters.Any(m => m.Status.Health > 0) && currentHealth > 0)
            {
                DisplayCombatStatus(player, monsters, currentHealth);
                //플레이 차례 (플레이는 무조건 우선순위)
                PlayerTurn(player, monsters, ref currentHealth);

                if (monsters.All(m => m.Status.Health <= 0))
                {
                    break;
                }
                MonsterTurn(player, monsters, ref currentHealth);
            }

            return currentHealth > 0;
        }

        private static void DisplayCombatStatus(Player player, List<Monster> monsters, int currentHealth)
        {
            Console.Clear();
            Console.WriteLine("========== 전투 ==========");
            Console.WriteLine($"{player.Name} HP: {currentHealth}/{player.Health}\n");
            Console.WriteLine(">> 적 목록:");
            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.Status.Health > 0 ? $"HP: {m.Status.Health}" : "(쓰러짐)";
                Console.WriteLine($"{i + 1}. {m.Name} - {status}");
            }
        }

        private static void PlayerTurn(Player player, List<Monster> monsters, ref int currentHealth)
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
                    PlayerAttack(player, monsters);
                    break;
                case "2":
                    // 스킬 사용하기
                    break;
                case "3":
                    // 아이템 사용
                    break;
                case "4":
                    // 도망가기
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        private static int RiggedRollByLuck(int luck)
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

        private static int CalculateDamage(int baseDamage, int luck)
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
        private static void PlayerAttack(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("\n공격할 대상을 선택하세요:");
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Status.Health > 0)
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].Status.Health})");
            }
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= monsters.Count &&
                monsters[choice - 1].Status.Health > 0)
            {
                Monster target = monsters[choice - 1];
                int damage = CalculateDamage(player.Status.Attack, player.Luck);
                target.Status.Health -= damage;

                if (target.Status.Health < 0) target.Status.Health = 0;

                Console.WriteLine($"\n{player.Name}이(가) {target.Name}에게 {damage}의 피해를 입혔습니다!");
                Thread.Sleep(3000);
            }

        }
        private static void PlayerSkill(Player player, List<Monster> monsters)
        {

        }

        private static void MonsterTurn(Player player, List<Monster> monsters, ref int currentHealth)
        {
            foreach (var monster in monsters.Where(m => m.Status.Health > 0))
            {
                //TODO
            }
        }
    }
}
