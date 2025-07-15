using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Team_SRRPG.Model
{
    [XmlInclude(typeof(ArmorItem))]
    [XmlInclude(typeof(WeaponItem))]
    public class Item
    {
        [XmlAttribute("Id")]    //XML의 속성으로 지정.
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int Cost { get; set; }
        public int Luck { get; set; }

        public bool isSale { get; set; }    // 해당 아이템을 상점에 판매하는가
        public bool isEquiped { get; set; } // 장비 장착여부

        public Item()
        {

        }

        public Item(int id, string name, string description, Status status, int cost = 0, int luck = 0)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            Cost = cost;
            Luck = luck;
        }
    }
}
