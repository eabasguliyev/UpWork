using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UpWork.Abstracts;
using UpWork.Exceptions;

namespace UpWork.Entities
{
    public class Employer : User
    {
        public IList<Vacancy> Vacancies { get; }
        public Employer()
        {
            Vacancies = new List<Vacancy>();
        }


        public void ShowAllAds(bool onlyPublic = false)
        {
            if (Vacancies.Count == 0)
                throw new AdException("There is no Vacancies!");

            if (onlyPublic)
            {
                foreach (var vacancy in Vacancies)
                {
                    if (vacancy.IsPublic)
                    {
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine(vacancy);
                    }
                }
            }
            else
            {
                foreach (var vacancy in Vacancies)
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine(vacancy);
                }
            }
        }

        public void ShowAllAdsWithRequestCount()
        {
            if (Vacancies.Count == 0)
                throw new AdException("There is no Vacancies!");

            foreach (var vacancy in Vacancies)
            {
                Console.WriteLine("--------------------------------------");
                vacancy.ShowVacancyWithRequestCount();
            }
        }

        public void DeleteVacancy(Guid guid)
        {
            if (Vacancies.Count == 0)
                throw new VacancyException("There is no Vacancies!");

            var vacancy = Vacancies.SingleOrDefault(v => v.Guid == guid);

            if (vacancy == null)
                throw new VacancyException($"There is no Vacancy associated this guid -> {guid}");

            Vacancies.Remove(vacancy);
        }

        public Vacancy GetVacancy(Guid guid)
        {
            var vacancy = Vacancies.SingleOrDefault(v => v.Guid == guid);

            if (vacancy == null)
                throw new VacancyException($"There is no vacancy associated this guid -> {guid}");

            return vacancy;
        }
    }
}