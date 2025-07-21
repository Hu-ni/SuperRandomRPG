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
        AllAttack,
        WAttack,
        GAttack
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
                Name = "방패들기",
                Description = "방패를 들어 방어력을 높입니다.",
                ManaCost = 8,
                Type = SkillType.Defense,
                Power = 10,
            },
            new Skill
            {
                Name = "지진",
                Description = "모든 적에게 피해를 가합니다",
                ManaCost = 15,
                Type = SkillType.AllAttack,
                Power = 8
            },
            new Skill
            {
                Name = "방패밀치기",
                Description = "방어력 수치만큼 상대에게 피해를 입힙니다.",
                ManaCost = 12,
                Type = SkillType.WAttack,
                Power = 10
            },
        };
        public List<Skill> MageSkill { get; set; } = new List<Skill>
        {
            new Skill
            {
                Name = "파이어볼",
                Description = "강력한 화염 마법을 상대에게 발사합니다",
                ManaCost = 20,
                Type = SkillType.Attack,
                Power = 20
            },
            new Skill
            {
                Name = "아이스 블래스트",
                Description = "광역 얼음 마법을 시전합니다.",
                ManaCost = 18,
                Type = SkillType.AllAttack,
                Power = 10
            },
            new Skill
            {
                Name = "회복",
                Description = "체력을 회복합니다.",
                ManaCost = 10,
                Type = SkillType.Healing,
                Power = 15
            },
        };
        public List<Skill> Archer { get; set; } = new List<Skill>
        {
            new Skill
            {
                Name = "화살비",
                Description = "무수한 화살을 쏘아올립니다.",
                ManaCost = 20,
                Type = SkillType.AllAttack,
                Power = 10
            },
            new Skill
            {
                Name = "속사",
                Description = "빠르게 화살을 발사합니다.",
                ManaCost = 12,
                Type = SkillType.Attack,
                Power = 20
            },
            new Skill
            {
                Name = "구르기",
                Description = "민첩하게 움직여서 피해를 최소화합니다.",
                ManaCost = 15,
                Type = SkillType.Defense,
                Power = 5 //행운을 증가시키는 효과
            },
        };
        public List<Skill> Gambler { get; set; } = new List<Skill>
        { 
            new Skill
            {
                Name = "네잎클로버 발견",
                Description = "땅바닥에서 행운의 네잎클로버를 발견하셨습니다.",
                ManaCost = 5,
                Type = SkillType.Luck,
                Power = 5
            },
            new Skill
            {
                Name = "코인어택",
                Description = "투자한 코인만큼 상대에게 피해를 입힙니다.",
                ManaCost = 20,
                Type = SkillType.GAttack,
                Power = 1
            },
            new Skill
            {
                Name = "와일드카드",
                Description = "카드를 사용하여 적에게 피해를 입히거나.",
                ManaCost = 15,
                Type = SkillType.Attack,
                Power = 10
            }
        };


        Dictionary<Job, List<Skill>> skillList = new Dictionary<Job, List<Skill>>();
        public SkillRepository()
        {
            skillList.Add(Job.Warrior, WarriorSkill);
            skillList.Add(Job.Mage, MageSkill);
            skillList.Add(Job.Archer, Archer);
            skillList.Add(Job.Gambler, Gambler);
        }


        public List<Skill> GetSkillsByJob(Job job)
        {
            return skillList[job];
        }
    }
}
