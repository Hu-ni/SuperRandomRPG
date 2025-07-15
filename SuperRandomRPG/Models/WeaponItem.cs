using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public enum WeaponType
    {
        Sword,
        Spear,
        Knife
    };

    public class WeaponItem : Item
    {
        public WeaponType Type { get; set; }

    }
}
