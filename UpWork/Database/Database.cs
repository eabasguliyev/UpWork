using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UpWork.Abstracts;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Database
{
    public class Database
    {
        public static bool Changes { get; set; }
        public IList<User> Users { get; set; }

        public Database()
        {
            Users = new List<User>();
        }

        public bool CheckMail(string mail)
        {
            return Users.SingleOrDefault(u => u.Mail.Equals(mail)) == null;
        }

        public bool CheckUsername(string username)
        {
            return Users.SingleOrDefault(u => u.Username.Equals(username)) == null;
        }

        public IList<Vacancy> GetVacancies()
        {
            var flag = false;

            var vacancies = new List<Vacancy>();

            foreach (var user in Users)
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

        public IList<Worker> GetWorkers()
        {
            var flag = false;

            var workers = new List<Worker>();

            foreach (var user in Users)
            {
                if (user is Worker worker)
                {
                    workers.Add(worker);
                    flag = true;
                }
            }

            if (!flag)
                throw new DatabaseException("There is no worker!");

            return workers;
        }

        public IList<Cv> GetCvs()
        {
            var flag = false;

            var cvs = new List<Cv>();

            foreach (var user in Users)
            {
                if (user is Worker worker)
                {
                    foreach (var cv in worker.Cvs)
                    {
                        cvs.Add(cv);
                        flag = true;
                    }
                }
            }

            if (!flag)
                throw new CvException("There is no cv!");


            return cvs;
        }

        public IList<Employer> GetEmployers()
        {
            var flag = false;

            var employers = new List<Employer>();

            foreach (var user in Users)
            {
                if (user is Employer employer)
                {
                    employers.Add(employer);
                    flag = true;
                }
            }

            if (!flag)
                throw new DatabaseException("There is no employer!");

            return employers;
        }

        public List<Cv> GetAllCvFromRequests(Dictionary<Guid, Guid> requests)
        {
            var cvs = new List<Cv>();

            foreach (var item in requests)
            {
                if (Users.SingleOrDefault(u => u.Guid == item.Key) is Worker worker)
                {
                    if (worker.Cvs.SingleOrDefault(c => ((Cv)c).Guid == item.Value) is Cv cv)
                        cvs.Add(cv);
                }
            }

            return cvs;
        }

        public List<Vacancy> GetAllVacanciesFromRequests(Dictionary<Guid, Guid> requests)
        {
            var vacancies = new List<Vacancy>();

            foreach (var item in requests)
            {
                if (Users.SingleOrDefault(u => u.Guid == item.Key) is Employer employer)
                {
                    vacancies.Add(employer.Vacancies.SingleOrDefault(v => (v.Guid == item.Value)));
                }
            }

            return vacancies;
        }
    }
}