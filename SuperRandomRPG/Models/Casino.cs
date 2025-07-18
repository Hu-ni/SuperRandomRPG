using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public class Casino
    {
        public Player Player { get; set; }
        public void OpenCasino()
        {
            Console.Clear();
            Console.WriteLine("카지노에 오신 것을 환영합니다!");
            Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
            Console.WriteLine("베팅할 선택지를 입력하세요. 0을입력하면 카지노를 나갈 수 있습니다.");
            Console.WriteLine("1. 500 Gold 베팅 \n50% 확률로 900 Gold 획득 또는 500 Gold 손실");
            Console.WriteLine("2. 2000 Gold 베팅 \n60% 확률로 2000 Gold 손실, 30% 확률로 1500 Gold 획득, 10% 확률로 10000 Gold 획득");
            Betting();
        }
        //선택지에 따라 플레이어의 일부 골드가 감소하고 확률에 따라 골드 획득 또는 손실
        //1번 선택지는 500골드를 지불하고 50%확률로 1000골드를 획득하거나 500골드를 잃는 선택지
        //2번 선택지는 2000골드를 지불하고 60%확률로 2000골드를 잃고 30% 확률로 1500골드를 획득하고
        //10% 확률로 10000골드를 획득하는 선택지
        public void Betting()
        {
            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                int roll = new Random().Next(1, 101);
                if (input == 1)
                {
                    if (roll < 51)
                    {
                        Console.WriteLine("축하합니다! 900골드를 획득했습니다.");
                        Player.Gold += 900;
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                    else
                    {
                        Console.WriteLine("아쉽습니다. 500골드를 잃었습니다.");
                        Player.Gold -= 500;
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                }
                if (input == 2)
                {
                    if (roll < 11)
                    {
                        Console.WriteLine("대박! 10000골드를 획득했습니다.");
                        Player.Gold += 10000;
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                    else if (roll >= 11 && roll < 41)
                    {
                        Console.WriteLine("축하합니다! 1500골드를 획득했습니다.");
                        Player.Gold += 1500;
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                    else if (roll >= 41 && roll < 101)
                    {
                        Console.WriteLine("아쉽습니다. 2000골드를 잃었습니다.");
                        Player.Gold -= 2000;
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                    else
                    {
                        Console.WriteLine("아쉽습니다. 아무것도 얻지 못했습니다.");
                        Console.WriteLine($"현재 플레이어 금액: {Player.Gold} Gold");
                    }
                }
                else if (input == 0)
                {
                    Console.Clear();
                    Console.WriteLine("카지노를 나갑니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }
            }
        }
    }
}
