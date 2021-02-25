using System;
using System.Collections.Generic;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Helpers
{
    public static class EmployerHelper
    {
        public static void ShowCvs(List<Cv> cvs)
        {
            if (cvs.Count == 0)
                throw new CvException("There is no cv!");

            foreach (var cv in cvs)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine(cv);
            }
        }
    }
}