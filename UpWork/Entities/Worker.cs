using System;
using System.Collections.Generic;
using System.Linq;
using UpWork.Abstracts;
using UpWork.Exceptions;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Worker:User
    {
        public IList<Cv> Cvs { get; set; }

        public Worker()
        {
            Cvs = new List<Cv>();
        }

        public void ShowAllCv(bool onlyPublic = false)
        {
            if (Cvs.Count == 0)
                throw new CvException("There is no cv!");


            if (onlyPublic)
            {
                var flag = false;

                foreach (var cv in Cvs)
                {
                    if (cv.IsPublic)
                    {
                        Console.WriteLine("--------------------------------------");

                        Console.WriteLine(cv);
                        flag = true;
                    }
                }

                if (!flag)
                    throw new CvException("There is no Cv!");
            }
            else
            {
                foreach (var cv in Cvs)
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine(cv);
                }
            }
        }

        public void DeleteCv(Guid guid)
        {
            var cv = Cvs.SingleOrDefault(c => ((Cv)c).Guid == guid);


            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");

            Cvs.Remove(cv);
        }

        public Cv GetCv(Guid guid)
        {
            var cv = Cvs.SingleOrDefault(c => ((Cv)c).Guid == guid);

            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");

            return cv;
        }

        public void ShowAllCvWithRequestCount()
        {
            if (Cvs.Count == 0)
                throw new CvException("There is no cv!");

            foreach (var cv in Cvs)
            {
                Console.WriteLine("---------------------------------");
                ((Cv)cv).ShowCvWithRequestCount();
            }
        }
    }
}