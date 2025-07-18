using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;
using static System.Collections.Specialized.BitVector32;

namespace SuperRandomRPG.Models
{
    public class Village
    {
        private Player _player;
        private Status _Status;
        private Inventory _inventory;
        private Shop _shop;
        private DungeonManager _dungeonManager;
        //private Inn _inn; // -> 여관 생성되면 추가

        public Village(Player player, Inventory inventory, Shop shop, DungeonManager dungeonManager)//, Inn inn)
        {
            _player = player;
            _inventory = inventory;
            _shop = shop;
            _dungeonManager = dungeonManager;
            //_inn = inn;
        }

        public void OpenVillage(Player player, Inventory inventory, Shop shop, DungeonManager dungeonManager)//, Inn inn)
        {
            Console.Clear();
            Console.WriteLine("===== 마을화면 =====");

            Console.WriteLine($"--------------------------------------------------------------------------- \n");

            Console.WriteLine("1.상태보기");
            Console.WriteLine("2.인벤토리");
            Console.WriteLine("3.상점");
            Console.WriteLine("4.던전");
            Console.WriteLine("5.여관(휴식) \n");

            Console.WriteLine($"--------------------------------------------------------------------------- \n");
            Console.WriteLine("0.나가기");
            Console.WriteLine(">>");
        }

        public void VillageSelection(string selection)
        {

            switch (selection)
            {
                case "1":
                    Console.WriteLine("상태창으로 이동합니다");
                    _player.OpenStatus(_inventory);
                    break;

                case "2":
                    Console.WriteLine("인벤토리로 이동합니다");
                    _inventory.OpenInventory(_player);
                    break;

                case "3":
                    Console.WriteLine("상점으로 이동합니다");
                    _shop.OpenShop(_player);
                    break;

                case "4":
                    Console.WriteLine("던전으로 이동합니다");
                    _dungeonManager.ShowDungeonSelectionScene();
                    break;

                //case "5":
                //     Console.WriteLine("여관(휴식)으로 이동합니다");
                //     _inn.OpenInn();
                //     break;

                case "0":
                    Console.WriteLine("나가기를 선택하셨습니다");
                    break;

                default:
                    Console.WriteLine("잘 못 누르셨습니다");
                    Console.ReadLine();
                    return;
            }
        }
    }
}
