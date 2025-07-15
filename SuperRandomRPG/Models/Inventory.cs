using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public class Inventory
    {
        public List<Item> Items { get; set; }

        public Item Find(int id) => Items.FirstOrDefault(x => x.Id == id);
        public List<Item> FindEquiped() => Items.FindAll(x => x.isEquiped);
    }
}
