using System;
using System.Collections.Generic;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Vacancy:Id
    {
        public Contact Contact { get; set; }
        public Advert Ad { get; set; }

        public Dictionary<Guid, Guid> RequestsFromWorkers { get; set; }
        public Vacancy()
        {
            RequestsFromWorkers = new Dictionary<Guid, Guid>();
        }
        public override string ToString()
        {
            return $@"{base.ToString()}
Contact:
{Contact}
Ad:
{Ad}";
        }

        public void ShowVacancyWithRequestCount()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Request count: {RequestsFromWorkers.Count}");
        }
    }
}