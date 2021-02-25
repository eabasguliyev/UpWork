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

        public static IList<Worker> GetWorkers(this Database.Database database)
        {
            var flag = false;

            var workers = new List<Worker>();

            foreach (var user in database.Users)
            {
                if (user is Worker worker)
                {
                    workers.Add(worker);
                    flag = true;
                }
            }

            if (!flag)
                throw new CvException("There is no worker!");

            return workers;
        }

        public static List<Cv> GetAllCvFromRequests(this Database.Database database, Dictionary<Guid, Guid> requests)
        {
            var cvs = new List<Cv>();

            foreach (var item in requests)
            {
                if (database.Users.SingleOrDefault(u => u.Guid == item.Key) is Worker worker)
                {
                    if(worker.Cvs.SingleOrDefault(c => ((Cv)c).Guid == item.Value) is Cv cv)
                        cvs.Add(cv);
                }
            }

            return cvs;
        }
    }
}