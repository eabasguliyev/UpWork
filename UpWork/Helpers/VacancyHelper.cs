using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Exceptions;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class VacancyHelper
    {
        public static string InputData(string dataName)
        {
            Console.WriteLine($"{dataName}: ");

            return UserHelper.GetString($"{dataName}: can not be empty");
        }

        public static int InputSalary(string message)
        {
            
            Console.WriteLine(message);

            while (true)
            {
                var salary = (int)UserHelper.GetNumeric(NumericTypes.INT);

                if (salary < 0)
                {
                    LoggerPublisher.OnLogError("Salary must be greater than or equal to zero");
                    continue;
                }

                return salary;
            }
        }

        public static Vacancy GetVacancy(Guid vacId, IList<Vacancy> vacancies)
        {
            if (vacId == Guid.Empty)
                throw new ArgumentNullException(nameof(vacId), "Vacancy id is empty!");
            if (vacancies == null)
                throw new VacancyException("Vacancy is null");
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancy!");

            var vacancy = vacancies.SingleOrDefault(v => v.Guid == vacId);

            if (vacancy == null)
                throw new VacancyException($"There is no vacancy associated this id -> {vacId}");
            return vacancy;

            
        }

        public static void SeeAds(IList<Vacancy> vacancies)
        {
            if (vacancies == null || vacancies.Count == 0)
                throw new VacancyException("There is no vacancy!");


            foreach (var vacancy in vacancies)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(vacancy);
            }
        }
    }
}