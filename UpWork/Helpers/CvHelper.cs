using System;
using System.Collections.Generic;
using System.Linq;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Exceptions;
using UpWork.Interfaces;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class CvHelper
    {
        public static void SeeCvs(IList<Cv> cvs)
        {
            if (cvs == null)
                throw new ArgumentNullException(nameof(cvs));

            if (cvs.Count == 0)
                throw new VacancyException("There is no cv!");


            foreach (var cv in cvs)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(cv);
            }
        }
        public static string InputData(string dataName)
        {
            Console.WriteLine($"{dataName}: ");

            return UserHelper.GetString($"{dataName}: can not be empty");
        }

        public static int InputUniScore()
        {
            Console.WriteLine("Uni score: ");
            return UserHelper.GetNumeric(NumericTypes.INT);
        }

        public static SkillLevelEnum InputSkillLevel()
        {
            Console.WriteLine("Level: ");
            while (true)
            {
                ConsoleScreen.PrintMenu(ConsoleScreen.SkillLevels, ConsoleColor.Blue);

                return (SkillLevelEnum) (ConsoleScreen.Input(ConsoleScreen.SkillLevels.Count));
            }
        }

        public static DateTime InputDateTime(string message)
        {
            
            Console.WriteLine(message);

            while (true)
            {
                var str = UserHelper.GetString("Datetime can not be empty!");

                var result = DateTime.TryParse(str, out DateTime dateTime);

                if (result)
                    return dateTime;

                LoggerPublisher.OnLogError("Invalid date time format!");
            }
        }

        public static bool InputHonorsDiplomaStatus()
        {
            Console.WriteLine("Do you have honors diploma? (y/n)");
            var resp = UserHelper.GetString("Input can not be empty");

            return resp[0] == 'y';
        }
        public static string InputLink()
        {
            Console.WriteLine("Link: ");
            while (true)
            {
                var link = UserHelper.GetString("Link can not be empty");

                if(ExceptionHandle.Handle(UserHelper.ValidateLink, link))
                    return link;
            }
        }


        public static Cv GetCv(Guid cvId, IList<Cv> cvs)
        {
            var cv = cvs.SingleOrDefault(c => ((Cv)c).Guid == cvId);

            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {cvId}");

            return (Cv)cv;
        }
    }
}