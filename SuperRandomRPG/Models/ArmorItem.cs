using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public enum ArmorItem
    {
        Helmet,
        Chestplate,
        Boots
    }

    public class ArmorType : Item
    {
        public ArmorType Type { get; set; }

    }
}
