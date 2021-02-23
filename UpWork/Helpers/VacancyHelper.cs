using System;
using UpWork.Enums;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class VacancyHelper
    {
        public static string InputData(string dataName)
        {
            Console.WriteLine($"{dataName}: ");

            return UserHelper.GetString($"{dataName}: can not be empty");
        }

        public static int InputSalary(string message)
        {
            var logger = new ConsoleLogger();
            Console.WriteLine(message);

            while (true)
            {
                var salary = (int)UserHelper.GetNumeric(NumericTypes.INT);

                if (salary < 0)
                {
                    logger.Error("Salary must be greater than or equal to zero");
                    continue;
                }

                return salary;
            }
        }
    }
}