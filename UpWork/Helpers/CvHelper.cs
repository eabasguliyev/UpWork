using System;
using UpWork.ConsoleInterface;
using UpWork.Enums;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class CvHelper
    {
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
            var logger = new ConsoleLogger();
            Console.WriteLine(message);

            while (true)
            {
                var str = UserHelper.GetString("Datetime can not be empty!");

                var result = DateTime.TryParse(str, out DateTime dateTime);

                if (result)
                    return dateTime;
                
                logger.Error("Invalid date time format!");
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
    }
}