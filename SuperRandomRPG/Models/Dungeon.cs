using SuperRandomRPG.Models;
using System.Collections.Generic;

namespace Team_SRRPG.Model
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Difficult { get; set; }

        public List<Monster> Monsters { get; set; }
        public Reward Reward { get; set; }
    }
}
