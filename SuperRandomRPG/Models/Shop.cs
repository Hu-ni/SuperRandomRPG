using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace SuperRandomRPG.Models
{
    public enum Item
    {
        Sword,
        Spear,
        Knife,
    }

    public class Shop
    {
        public int Item { get; set; }
        public int Money { get; set; }
        public int Difficult { get; set; }
    }
    

        public static List<Shop> CreateDefaultShop() 
        {

            return new List<Shop>
            {
                new Shop
                {
                    Id = 1,
                    Name = "칼바람 나락",
                    Description  = "Placeholder",
                    Difficult = 1,
                    Shop = new List<Shop>
                }
            }    

        }


        
        
        
        
        
    
}
