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

        public Inventory()
        {
            Items = new List<Item>();
            {
                // 초기 아이템 추가 (예시)
                Items.Add(new Item(1, "목제 검", "나무로 만든 연습용 검입니다."+"공격력:+3", new Status {Attack = 3}, 100));
                Items.Add(new Item(2, "목제 보호구", "나무로 만든 연습용 보호구입니다."+"방어력+2", new Status {Defense = 2}, 80));
                Items.Add(new Item(3, "아무것도 아닌 팬던트", "누군가의 사진이 들어있다.", new Status {Attack = 0}, 50, 1));
            }
        }

        public void OpenInventory()
        {
            Console.WriteLine("인벤토리 창입니다. 0을 눌러 나갈 수 있습니다.");
            Console.WriteLine("아이템 목록:");
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                if(item.isEquiped)
                {
                    Console.WriteLine($"{i + 1}. [E]{item.Name} - {item.Description}");
                }
                else 
                {
                    Console.WriteLine($"{i + 1}. {item.Name} - {item.Description}");
                }          
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
                else if (input > 0 && input <= Items.Count)
                {
                    EquipItem(input); // 아이템을 장착하는 메서드 호출
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }

        //아이템 목록에서 인덱스-1로 아이템을 선택하면 bool값을 true로 바꾼다.
        public void EquipItem(int input)
        {
            int EquipIndex = input;
            if (EquipIndex < 1 || EquipIndex > Items.Count)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                return;
            }
            else
            {
                if (Items[EquipIndex - 1].isEquiped)
                {
                    Items[EquipIndex - 1].isEquiped = false;
                }
                else if(!Items[EquipIndex - 1].isEquiped)
                {
                    Items[EquipIndex - 1].isEquiped = true; // 아이템을 장착 상태로 변경
                }
            }
        }
    }
}
