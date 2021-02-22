using System.Collections.Generic;
using System.Security.Permissions;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Employer : User
    {
        public IList<Vacancy> Vacancies { get; }
        public Employer()
        {
            Vacancies = new List<Vacancy>();
        }
    }
}