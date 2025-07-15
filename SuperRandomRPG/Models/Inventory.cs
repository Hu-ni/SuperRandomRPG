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
    }
}
