using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Vacancy:Id
    {
        public Contact Contact { get; set; }
        public Advert Ad { get; set; }

        public override string ToString()
        {
            return $@"{base.ToString()}
Contact:
{Contact}
Ad:
{Ad}";
        }
    }
}