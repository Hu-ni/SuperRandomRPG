using SuperRandomRPG.Models;
using System;

namespace Team_SRRPG.Model
{
    public class Inn
    {
        private int _price;
        private static Random _rng = new();

        public Inn(int price = 50) // default price
        {
            _price = price;
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
            Console.WriteLine($"Result: {maxRoll}");
            return maxRoll;
        }

        public void EnterInn(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[여관] 체력과 마나를 회복하는 서비스입니다!");
                Console.WriteLine(@"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⢀⣀⣀⣀⠀⣶⣶⠀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⠀⣶⣶⠀⣀⣀⣀⡀⠀
            ⠀⠘⠛⠛⠛⠀⣿⣿⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠀⣿⣿⠀⠛⠛⠛⠃⠀
            ⠀⠀⠀⠀⠀⠀⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣤⣤⣤⣤⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣀⣀⣀⣀⣿⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⡿⠛⣛⠋⣠⣄⣉⣉⠙⣿⣿⣿⣿⣿⠿⠿⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⡇⢸⣿⡿⣿⡿⠹⣿⠇⠛⠻⣿⣿⣿⣶⣦⠀⠀⠀⠀
            ⠀⠀⠀⠀⣀⣤⣭⣿⣿⣿⣿⠄⢠⣤⣈⣁⣤⡄⠠⣶⡀⢹⣿⣿⣿⣿⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⣿⣿⣿⠀⣿⡇⢸⣿⣿⣿⣿⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⠀⣿⣿⣿⣿⣿⣿⠀⣿⠀⣼⣿⣿⣉⠉⠀⠀⠀⠀
            ⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⠐⠛⠛⠛⠛⠛⠛⠂⣠⣴⣿⣿⣿⣿⣷⠀⠀⠀⠀
            ⠀⠀⠀⠀⠛⠛⠻⢿⣿⣿⣿⠘⠛⠛⠛⠛⠛⠛⠃⣿⣿⣿⡿⠟⠛⠛⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⠿⠿⠿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
                Console.WriteLine($"\n\n{player.Name}님의 체력과 마나:\nHP:{player.Health}/{player.Status.Health}");
                Console.WriteLine($"Mana:{player.Mana}/{player.Status.Mana}");
                Console.WriteLine($"무엇을 하시겠습니까?\n\n");
                Console.WriteLine("1.휴식하기 (500G)\n2.도박하기\n\n0.나가기");
                Console.WriteLine(">>");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 1)
                    {
                        if (player.Gold < 500)
                        {
                            Console.Clear();
                            Console.WriteLine("\n[!] 소지금이 부족합니다.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        Console.Clear();
                        int luckroll = RiggedRollByLuck(player.TotalLuck);
                        if (luckroll == 1)
                        {
                            Console.WriteLine("도적에게 습격당했습니다! 체력을 30 잃고, 200G를 도둑맞았습니다!");
                            player.Health = Math.Max(0, player.Health - 30);
                            player.Gold = Math.Max(0, player.Gold - 200);
                        }
                        else if (luckroll >= 2 && luckroll <= 5)
                        {
                            Console.WriteLine("불편한 침대 때문에 몸살이 났습니다... 체력이 10 감소했습니다.");
                            player.Health = Math.Max(0, player.Health - 10);
                        }
                        else if (luckroll >= 6 && luckroll <= 14)
                        {
                            int healAmount = luckroll * 2;
                            int manaAmount = luckroll;
                            player.Health = Math.Min(player.Status.Health, player.Health + healAmount);
                            player.Mana = Math.Min(player.Status.Mana, player.Mana + manaAmount);
                            Console.WriteLine($"그럭저럭 쉴 수 있었습니다. 체력 {healAmount} / 마나 {manaAmount} 회복!");
                        }
                        else if (luckroll >= 15 && luckroll <= 19)
                        {
                            int healAmount = luckroll * 4;
                            int manaAmount = luckroll * 3;
                            player.Health = Math.Min(player.Status.Health, player.Health + healAmount);
                            player.Mana = Math.Min(player.Status.Mana, player.Mana + manaAmount);
                            Console.WriteLine($"푹 쉬었습니다! 체력 {healAmount} / 마나 {manaAmount} 회복!");
                        }
                        else if (luckroll == 20)
                        {
                            player.Health = player.Status.Health;
                            player.Mana = player.Status.Mana;
                            player.Gold += 100;
                            Console.WriteLine("최고의 휴식을 취했습니다! 체력과 마나가 모두 회복되었고, 100G를 침대 밑에서 발견했습니다!");
                        }
                        player.Gold -= 500;
                        Console.WriteLine($"\n현재 체력: {player.Health}/{player.Status.Health}");
                        Console.WriteLine($"현재 마나: {player.Mana}/{player.Status.Mana}");
                        Console.WriteLine($"남은 소지금: {player.Gold}G");
                        Thread.Sleep(3000);
                    }
                    else if (choice == 2)
                    {
                        //TODO Gamble
                    }
                    else if (choice == 0)
                    {
                        break; 
                    }
                }
            }
        }
    }
}
