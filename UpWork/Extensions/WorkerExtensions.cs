using System;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Sides;

namespace UpWork.Extensions
{
    public static class WorkerExtensions
    {
        public static void ShowAllCv(this Worker worker)
        {
            if (worker.Cvs.Count == 0)
                throw new NotFoundCvException("There is no cv!");

            foreach (var cv in worker.Cvs)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(cv);
            }
        }

        public static void DeleteCv(this Worker worker, Guid guid)
        {
            var index = worker.Cvs.ToList().FindIndex(c => ((Cv) c).Guid == guid);


            if (index < 0)
                throw new NotFoundCvException($"There is no cv associated this guid -> {guid}");
            
            worker.Cvs.RemoveAt(index);
        }
    }
}