using System;
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

    }
}