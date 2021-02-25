using System;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Extensions
{
    public static class VacancyExtensions
    {
        public static bool CheckWorkerRequest(this Vacancy vacancy, Guid workerId)
        {
            if (vacancy.RequestsFromWorkers.Count == 0)
                return false;

            var req = vacancy.RequestsFromWorkers.SingleOrDefault(i => i.Key == workerId);

            return req.Key != Guid.Empty;
        }

        public static void SendRequest(this Vacancy vacancy, Guid workerId, Guid CvId)
        {
            vacancy.RequestsFromWorkers.Add(workerId, CvId);
        }

        public static void CancelRequest(this Vacancy vacancy, Guid workerId)
        {
            vacancy.RequestsFromWorkers.Remove(workerId);
        }

        public static void RemoveRequest(this Vacancy vacancy, Guid workerId)
        {
            vacancy.RequestsFromWorkers.Remove(workerId);
        }
    }
}