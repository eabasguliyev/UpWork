using System;
using System.Collections.Generic;
using System.Linq;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Vacancy:Id
    {
        public bool IsPublic { get; set; } = true;
        public Contact Contact { get; set; }
        public Advert Ad { get; set; }
        public int Views { get; set; }

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
{Ad}

View(s): {Views}";
        }

        public void ShowVacancyWithRequestCount()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Request count: {RequestsFromWorkers.Count}");
        }

        public bool CheckWorkerRequest(Guid workerId)
        {
            if (RequestsFromWorkers.Count == 0)
                return false;

            var req = RequestsFromWorkers.SingleOrDefault(i => i.Key == workerId);

            return req.Key != Guid.Empty;
        }

        public void SendRequest(Guid workerId, Guid CvId)
        {
            RequestsFromWorkers.Add(workerId, CvId);
        }

        public void RemoveRequest(Guid workerId)
        {
            RequestsFromWorkers.Remove(workerId);
        }

        public static Vacancy operator ++(Vacancy vacancy)
        {
            vacancy.Views++;

            return vacancy;
        }
    }
}