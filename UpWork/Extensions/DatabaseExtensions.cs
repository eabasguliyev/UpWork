using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Extensions
{
    public static class DatabaseExtension
    {
        public static bool CheckMail(this Database.Database database, string mail)
        {
            return database.Users.SingleOrDefault(u => u.Mail.Equals(mail)) == null;
        }

        public static bool CheckUsername(this Database.Database database, string username)
        {
            return database.Users.SingleOrDefault(u => u.Username.Equals(username)) == null;
        }

        public static IList<Vacancy> GetVacancies(this Database.Database database)
        {
            var flag = false;

            var vacancies = new List<Vacancy>();

            foreach (var user in database.Users)
            {
                if (user is Employer employer)
                {
                    foreach (var vacancy in employer.Vacancies)
                    {
                        vacancies.Add(vacancy);
                        flag = true;
                    }
                }
            }

            if (!flag)
                throw new VacancyException("There is no vacancy!");


            return vacancies;
        }
    }
}