using System;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Interfaces;
using UpWork.Sides;

namespace UpWork.Extensions
{
    public static class WorkerExtensions
    {
        public static void ShowAllCv(this Worker worker)
        {
            if (worker.Cvs.Count == 0)
                throw new CvException("There is no cv!");

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
                throw new CvException($"There is no cv associated this guid -> {guid}");
            
            worker.Cvs.RemoveAt(index);
        }

        public static ICv GetCv(this Worker worker, Guid guid)
        {
            var index = worker.Cvs.ToList().FindIndex(c => ((Cv) c).Guid == guid);

            if (index < 0)
                throw new CvException($"There is no cv associated this guid -> {guid}");

            return worker.Cvs[index];
        }
    }
}