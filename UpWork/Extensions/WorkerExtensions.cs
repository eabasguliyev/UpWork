using System;
using System.Collections.Generic;
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
            var cv = worker.Cvs.SingleOrDefault(c => ((Cv) c).Guid == guid);


            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");
            
            worker.Cvs.Remove(cv);
        }

        public static ICv GetCv(this Worker worker, Guid guid)
        {
            var cv = worker.Cvs.SingleOrDefault(c => ((Cv) c).Guid == guid);

            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");

            return cv;
        }

        public static void SeeAds(this Worker worker, IList<Vacancy> vacancies)
        {
            if (vacancies == null)
                throw new ArgumentNullException(nameof(vacancies));

            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancy!");


            foreach (var vacancy in vacancies)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(vacancy);
            }
        }

    }
}