using SuperRandomRPG.Models;
using System;

namespace Team_SRRPG.Model
{
    public class Inn
    {
        private int _price;

        public Inn(int price = 50) // default price
        {
            _price = price;
        }

        public void EnterInn(Player player)
        {
            Console.Clear();
            Console.WriteLine($"[여관] 체력과 마나를 회복하는 서비스입니다!");
        }
    }
}
