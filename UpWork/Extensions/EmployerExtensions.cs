using System;
using System.Collections.Generic;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Sides.Employee;

namespace UpWork.Extensions
{
    public static class EmployerExtensions
    {
        public static void ShowAllAds(this Employer employer)
        {
            if (employer.Vacancies.Count == 0)
                throw new AdException("There is no Vacancies!");

            foreach (var vacancy in employer.Vacancies)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(vacancy);
            }
        }

        public static void ShowAllAdsWithRequestCount(this Employer employer)
        {
            if (employer.Vacancies.Count == 0)
                throw new AdException("There is no Vacancies!");

            foreach (var vacancy in employer.Vacancies)
            {
                Console.WriteLine("--------------------------------------");
                vacancy.ShowVacancyWithRequestCount();
            }
        }

        public static void DeleteVacancy(this Employer employer, Guid guid)
        {
            if (employer.Vacancies.Count == 0)
                throw new VacancyException("There is no Vacancies!");

            var vacancy = employer.Vacancies.SingleOrDefault(v => v.Guid == guid);

            if (vacancy == null)
                throw new VacancyException($"There is no Vacancy associated this guid -> {guid}");

            employer.Vacancies.Remove(vacancy);
        }

        public static Vacancy GetVacancy(this Employer employer, Guid guid)
        {
            var vacancy = employer.Vacancies.SingleOrDefault(v => v.Guid == guid);

            if (vacancy == null)
                throw new VacancyException($"There is no vacancy associated this guid -> {guid}");

            return vacancy;
        }
    }
}