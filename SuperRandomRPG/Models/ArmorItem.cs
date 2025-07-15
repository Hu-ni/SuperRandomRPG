using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public enum ArmorType
    {
        Helmet,
        Chestplate,
        Boots
    }

    public class ArmorItem : Item
    {
        [XmlAttribute("Type")]    //XML의 속성으로 지정.
        public ArmorType Type { get; set; }

    }
}
