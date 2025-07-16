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
            int currentHealth = player.Health;

            while (monsters.Any(m => m.Status.Health > 0) && currentHealth > 0)
            {
                DisplayCombatStatus(player, monsters, currentHealth);
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
                    // 공격하기 시스템
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

        private static void MonsterTurn(Player player, List<Monster> monsters, ref int currentHealth)
        {
            foreach (var monster in monsters.Where(m => m.Status.Health > 0))
            {
                //TODO
            }
        }
    }
}
