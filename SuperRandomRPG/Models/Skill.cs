using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_SRRPG.Model;

namespace SuperRandomRPG.Models
{
    public enum SkillType
    {
        Attack,
        Defense,
        Healing,
        Luck,
    }
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManaCost { get; set; }
        public SkillType Type { get; set; }
        public int Power { get; set; }
    }

    // Fix: Move the SkillList property inside a class or remove the misplaced closing brace.  
    internal class SkillRepository
    {
        public List<Skill> WarriorSkill { get; set; } = new List<Skill>
        {
            new Skill
            {
                Name = "Shield",
                Description = "A defensive spell that increases the caster's defense.",
                ManaCost = 8,
                Type = SkillType.Defense,
                Power = 10,
            },
            new Skill
            {
                Name = "Earthquake",
                Description = "Causes a tremor that damages all enemies.",
                ManaCost = 15,
                Type = SkillType.Attack,
                Power = 30
            },
            new Skill
            {
                Name = "Berserk",
                Description = "Increases attack power at the cost of defense.",
                ManaCost = 12,
                Type = SkillType.Attack,
                Power = 25
            },
        };
        public List<Skill> MageSkill { get; set; } = new List<Skill>
        {
            new Skill
            {
                Name = "Fireball",
                Description = "A powerful fire spell that deals damage to a single target.",
                ManaCost = 20,
                Type = SkillType.Attack,
                Power = 40
            },
            new Skill
            {
                Name = "Ice Blast",
                Description = "A chilling spell that slows down enemies and deals damage.",
                ManaCost = 18,
                Type = SkillType.Attack,
                Power = 35
            },
            new Skill
            {
                Name = "Heal",
                Description = "A restorative spell that heals the caster.",
                ManaCost = 10,
                Type = SkillType.Healing,
                Power = 25
            },
        };
        public List<Skill> Archer { get; set; } = new List<Skill>
        {
            new Skill
            {
                Name = "Arrow Rain",
                Description = "Fires a barrage of arrows at all enemies.",
                ManaCost = 20,
                Type = SkillType.Attack,
                Power = 30
            },
            new Skill
            {
                Name = "Quick Shot",
                Description = "A rapid shot that deals damage to a single target.",
                ManaCost = 12,
                Type = SkillType.Attack,
                Power = 20
            },
            new Skill
            {
                Name = "Stealth",
                Description = "Allows the archer to become invisible for a short time.",
                ManaCost = 15,
                Type = SkillType.Luck,
                Power = 10 //행운을 증가시키는 효과
            },
        };
        Dictionary<string, List<Skill>> skillList = new Dictionary<string, List<Skill>>();
        public SkillRepository()
        {
            skillList.Add("Warrior", WarriorSkill);
            skillList.Add("Mage", MageSkill);
            skillList.Add("Archer", Archer);
        }
        public List<Skill> GetSkillsByJob(Job job)
        {
            return job switch
            {
                Job.Warrior => WarriorSkill,
                Job.Mage => MageSkill,
                Job.Archer => Archer,
                _ => new List<Skill>()
            };
        }
    }
}
