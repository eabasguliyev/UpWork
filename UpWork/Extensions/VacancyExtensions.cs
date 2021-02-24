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

            var req = vacancy.RequestsFromWorkers.SingleOrDefault(i => i == workerId);

            if (req == Guid.Empty)
                return false;

            return true;
        }

        public static void SendRequest(this Vacancy vacancy, Guid workerId)
        {
            vacancy.RequestsFromWorkers.Add(workerId);
        }

        public static void CancelRequest(this Vacancy vacancy, Guid workerId)
        {
            vacancy.RequestsFromWorkers.Remove(workerId);
        }
    }
}