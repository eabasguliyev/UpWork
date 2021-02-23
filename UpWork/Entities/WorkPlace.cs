using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class WorkPlace:Id
    {
        public string Company { get; set; }
        public Timeline Timeline { get; set; }


        public override string ToString()
        {
            return $@"{base.ToString()}
Company: {Company}
{Timeline}";
        }
    }
}