using System;
using System.Collections.Generic;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Vacancy:Id
    {
        public Contact Contact { get; set; }
        public Advert Ad { get; set; }

        public IList<Guid> RequestsFromWorkers { get; set; }

        public Vacancy()
        {
            RequestsFromWorkers = new List<Guid>();
        }
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