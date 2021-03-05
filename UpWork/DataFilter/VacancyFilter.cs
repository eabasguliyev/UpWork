using System.Collections.Generic;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.DataFilter
{
    public static class VacancyFilter
    {
        public static IList<Vacancy> FilterByCategory(string category, IList<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancie!");

            return vacancies.Where(v => v.Ad.Category == category).ToList();
        }

        public static IList<Vacancy> FilterByRegion(string region, IList<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancie!");

            return vacancies.Where(v => v.Ad.Region == region).ToList();
        }

        public static IList<Vacancy> FilterBySalary(int salary, IList<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancie!");

            return vacancies.Where(v => v.Ad.Salary >= salary).ToList();
        }

        public static IList<Vacancy> FilterByExperience(string experience, IList<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancie!");

            return vacancies.Where(v => v.Ad.Experience == experience).ToList();
        }

        public static IList<Vacancy> FilterByEducation(string education, IList<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new VacancyException("There is no vacancie!");

            return vacancies.Where(v => v.Ad.Education == education).ToList();
        }
    }
}