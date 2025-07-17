using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using Team_SRRPG.Model;
using static System.Formats.Asn1.AsnWriter;


namespace SuperRandomRPG.Models
{
    public class Shop
    {
        private readonly List<Item> ShopItems;
        private Inventory _inventory;
        


        public Shop(Inventory inven)
        {
            
            _inventory = inven;

            // 아이템 스테이터스 설정
            ShopItems = new List<Item>();
            
            Item sword = new Item
            {
                Id = 1,
                Name = "낡은 검",
                Description = "오래된 나무로 만든 작고 소중한 검",
                Cost = 100,
                Luck = 30,

                Status = new Status { Attack = 3,}

            };
            ShopItems.Add(sword);

            Item shield = new Item
            {
                Id = 2,
                Name = "무쇠 갑옷",
                Description = "무쇠로 만든 조약한 갑옷",
                Cost = 200,
                Luck = 40,

                Status = new Status { Health = 10, Attack = 0, Defense = 5,}

            };
            ShopItems.Add(shield);
        }

        // 인벤토리에서 아이템 구매
        public void OpenShop(Player player) 
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("=== 상점 ===");
                Console.WriteLine($"보유 골드: {player.Gold}\n");

                Console.WriteLine("---------------------------------------------------------------------- \n");

                // 아이템 스테이터스 정렬
                for (int i = 0; i < ShopItems.Count; i++)
                {
                    var item = ShopItems[i];
 
                    Console.WriteLine($"{i + 1}. {item.Id} - {item.Name}G.{item.Description}.{item.Cost}.{item.Luck} " +
                        $"(공격력: {item.Status.Attack}, 방어력: {item.Status.Defense},체력: {item.Status.Health})");
                }

                Console.WriteLine("---------------------------------------------------------------------- \n");

                Console.WriteLine("0. 나가기");
                Console.Write("\n구입할 아이템 번호를 입력하세요: ");
                string input = Console.ReadLine();


                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        Console.WriteLine("\n 상점에서 나갑니다.");
                        break;
                    }

                    if (choice >= 1 && choice <= ShopItems.Count)
                    {
                        var selected = ShopItems[choice - 1];
                        
                        if (player.Gold >= selected.Cost) 
                        {
                            _inventory.Items.Add(selected);
                            Console.WriteLine($"{selected.Name} 구매 완료");

                            player.Gold -= selected.Cost; // 플레이어 Gold감소
                            selected.Name += "구매완료"; // 구매 아이템 "구매완료" 표시
                        }
                        
                        else
                        {
                            Console.WriteLine("\n 돈이 부족합니다!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("\n 올바른 번호를 입력하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("\n 숫자를 입력해주세요.");
                }

                Console.WriteLine("\n 아무 키나 누르면 계속...");
                Console.ReadKey();
            }
        }

        // 인벤토리에서 아이템 판매
        private void SellItemMenu(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 아이템 판매 ===");
                Console.WriteLine($"보유 골드: {player.Gold}\n");

                if (_inventory.Items.Count == 0)
                {
                    Console.WriteLine("판매할 아이템이 없습니다.");
                    Console.WriteLine("\n아무 키나 누르면 돌아갑니다...");
                    Console.ReadKey();
                    break;
                }

                for (int i = 0; i < _inventory.Items.Count; i++)
                {
                    var item = _inventory.Items[i];
                    int sellPrice = item.Cost / 2;
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Description}.{item.Cost}.{item.Luck} " +
                        $"(공격력: {item.Status.Attack}, 방어력: {item.Status.Defense},체력: {item.Status.Health})");
                }

                Console.WriteLine("0. 뒤로 가기");
                Console.Write("\n판매할 아이템 번호를 입력하세요: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0) break;

                    if (choice >= 1 && choice <= _inventory.Items.Count)
                    {
                        var selected = _inventory.Items[choice - 1];
                        int sellPrice = selected.Cost / 2;

                        player.Gold += sellPrice;
                        _inventory.Items.RemoveAt(choice - 1);
                        Console.WriteLine($"\n {selected.Name} {sellPrice}G 판매완료.");
                    }
                    else
                    {
                        Console.WriteLine("\n 올바른 번호를 입력하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("\n 숫자를 입력해주세요.");
                }

                Console.WriteLine("\n 아무 키나 누르면 계속...");
                Console.ReadKey();
            }
        }

    }   
}
    



