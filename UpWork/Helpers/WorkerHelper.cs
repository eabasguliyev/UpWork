using System;
using System.Collections.Generic;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Helpers
{
    public static class WorkerHelper
    {
        public static void ShowVacancies(List<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new CvException("There is no vacancy!");

            foreach (var vacancy in vacancies)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine(vacancy);
            }
        }
    }
}