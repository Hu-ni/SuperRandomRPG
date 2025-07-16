using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    [XmlRoot("Inventory")]
    public class Inventory
    {
        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<Item> Items { get; set; }

        public Item Find(int id) => Items.FirstOrDefault(x => x.Id == id);
        public List<Item> FindEquiped() => Items.FindAll(x => x.isEquiped);

        public void OpenInventory()
        {
            Console.WriteLine("인벤토리 창입니다. 0을 눌러 나갈 수 있습니다.");
            Console.WriteLine("아이템 목록:");
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Description}");
            }
            Console.WriteLine("0. 나가기");
            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                if (input == 0)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
    }
}
