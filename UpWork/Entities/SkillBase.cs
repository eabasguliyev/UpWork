using UpWork.Enums;

namespace UpWork.Entities
{
    public abstract class SkillBase
    {
        public string Name { get; set; }
        public SkillLevelEnum Level { get; set; }

        public override string ToString()
        {
            return $@"Name: {Name}
Level: {Level}";
        }
    }
}