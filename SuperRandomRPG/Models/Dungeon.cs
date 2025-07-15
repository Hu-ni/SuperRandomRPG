using SuperRandomRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Team_SRRPG.Model
{
    // 데이터 저장될 그릇
    // 최소 단위 *
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficult { get; set; }

        public List<Monster> Monsters { get; set; }

        // 던전 클리어 보상
        public Reward Reward { get; set; }
    }
}
