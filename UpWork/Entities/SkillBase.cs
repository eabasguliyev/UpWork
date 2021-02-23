using UpWork.Abstracts;
using UpWork.Enums;

namespace UpWork.Entities
{
    public abstract class SkillBase:Id
    {
        public string Name { get; set; }
        public SkillLevelEnum Level { get; set; }

        public override string ToString()
        {
            return $@"{base.ToString()}
Name: {Name}
Level: {Level}";
        }
    }
}