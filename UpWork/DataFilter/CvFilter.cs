using System.Collections.Generic;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.DataFilter
{
    public static class CvFilter
    {
        public static IList<Cv> FilterByCategory(string category, IList<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            return cvs.Where(c => c.Category == category).ToList();
        }

        public static IList<Cv> FilterByRegion(string region, IList<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            return cvs.Where(c => c.Region == region).ToList();
        }

        public static IList<Cv> FilterBySalary(int salary, IList<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            return cvs.Where(c => c.Salary >= salary).ToList();
        }

        public static IList<Cv> FilterByExperience(string experience, IList<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            return cvs.Where(c => c.Experience == experience).ToList();
        }

        public static IList<Cv> FilterByEducation(string education, IList<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            return cvs.Where(c => c.Education == education).ToList();
        }
    }
}