using System;
using System.Text.RegularExpressions;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Exceptions;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class UserHelper
    {
        public static string GetString(string errorMessage)
        {
            var logger = new ConsoleLogger();
            while (true)
            {
                Console.Write(">> ");

                var str = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(str))
                    return str;

                logger.Error(errorMessage);
            }
        }

        public static dynamic GetNumeric(NumericTypes type)
        {
            var logger = new ConsoleLogger();

            switch (type)
            {
                case NumericTypes.INT:
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write(">> ");
                            
                            var data = Convert.ToInt32(Console.ReadLine());

                            return data;
                        }
                        catch (Exception)
                        {
                            logger.Error("Input must be numeric value!");
                        }
                    }
                }
                case NumericTypes.DOUBLE:
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write(">> ");

                            var data = Convert.ToDouble(Console.ReadLine());

                            return data;
                        }
                        catch (Exception)
                        {
                            logger.Error("Input must be numeric value!");
                        }
                    }
                }
                default:
                {
                    throw new InvalidOperationException("Invalid type!");
                }
            }

        }

        public static string GenerateCode()
        {
            var random = new Random();

            return random.Next(1000, 9999).ToString();
        }

        public static void ValidateMail(string mail)
        {
            var mailComponents = mail.Split('@');

            if (mailComponents.Length == 2 && mailComponents[1].Contains("."))
                return;

            throw new MailException("Invalid mail format");
        }

        public static void ValidatePhone(string phone)
        {
            foreach (var character in phone)
            {
                if (char.IsLetter(character))
                    throw new InvalidPhoneFormat("Phone number can not contain any letter!");
            }
        }

        public static void ValidateLink(string link)
        {
            if (link.StartsWith("https://"))
                return;

            throw new InvalidLinkFormat("Link format is not valid!");
        }

        public static Guid InputGuid()
        {
            var logger = new ConsoleLogger();

            Console.WriteLine("Enter guid: ");
            while (true)
            {
                var str = GetString("Guid can not be empty!");

                if (Guid.TryParse(str, out Guid guid))
                    return guid;
                

                logger.Error("Invalid guid format!");
            }
        }



        public static string InputCategory()
        {
            Console.Clear();
            Console.WriteLine("Category:");
            ConsoleScreen.PrintMenu(Data.Data.Categories, ConsoleColor.Blue);

            return Data.Data.Categories[ConsoleScreen.Input(Data.Data.Categories.Count) - 1];
        }

        public static string InputRegion()
        {
            Console.Clear();
            Console.WriteLine("Region:");
            ConsoleScreen.PrintMenu(Data.Data.Regions, ConsoleColor.Blue);

            return Data.Data.Regions[ConsoleScreen.Input(Data.Data.Regions.Count) - 1];
        }

        public static string InputEducation()
        {
            Console.Clear();
            Console.WriteLine("Education:");
            ConsoleScreen.PrintMenu(Data.Data.Educations, ConsoleColor.Blue);

            return Data.Data.Educations[ConsoleScreen.Input(Data.Data.Educations.Count) - 1];
        }

        public static string InputExperience()
        {
            Console.Clear();
            Console.WriteLine("Experience:");
            ConsoleScreen.PrintMenu(Data.Data.Experiences, ConsoleColor.Blue);

            return Data.Data.Experiences[ConsoleScreen.Input(Data.Data.Experiences.Count) - 1];
        }

        public static string InputSalary()
        {
            Console.Clear();
            Console.WriteLine("Salary: ");
            ConsoleScreen.PrintMenu(Data.Data.Salaries, ConsoleColor.Blue);

            return Data.Data.Salaries[ConsoleScreen.Input(Data.Data.Salaries.Count) - 1];
        }

        public static SalaryRange InputSalaryRange()
        {
            var salaryRange = new SalaryRange();

            Console.WriteLine("Salary range:");

            Console.WriteLine("From: ");
            salaryRange.From = GetNumeric(NumericTypes.INT);

            while (true)
            {
                Console.WriteLine("To: ");
                salaryRange.To = GetNumeric(NumericTypes.INT);

                if (salaryRange.To >= salaryRange.From)
                    break;

                var logger = new ConsoleLogger();
                logger.Error("Max salary must be greater than min!");
            }

            return salaryRange;
        }

        public static int ParseSalary(string data)
        {
            return Convert.ToInt32(Regex.Match(data, @"\d+").Value);
        }
    }
}