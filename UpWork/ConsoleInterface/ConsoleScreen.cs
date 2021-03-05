using System;
using System.Collections.Generic;
using UpWork.Logger;
using System.Windows.Forms;
namespace UpWork.ConsoleInterface
{
    public static class ConsoleScreen
    {
        public static IList<string> UserAccess { get; }
        public static IList<string> UserType { get; }
        public static IList<string> WorkerSideMainMenu { get; }
        public static IList<string> CvSectionMenu { get; }
        public static IList<string> SkillLevels { get; }
        public static IList<string> CvUpdateMenu { get; }

        public static IList<string> EmployerSideMainMenu { get; }
        public static IList<string> AdsSectionMenu { get; }
        public static IList<string> VacancyUpdateMenu { get; }

        public static IList<string> CvAdsChoices { get; }
        public static IList<string> FilterMenu { get; }

        static ConsoleScreen()
        {
            UserAccess = new List<string>() {"Login", "Register", "Exit"};
            UserType = new List<string>() {"Worker", "Employer"};
            WorkerSideMainMenu = new List<string>() {"See ads", "Your CV", "Cv Notifications", "See Notifications", "Logout"};
            CvSectionMenu = new List<string>() {"Show", "Add", "Update", "Delete", "Back"};
            SkillLevels = new List<string>() {"Beginner", "Intermediate", "Advanced"};
            CvUpdateMenu = new List<string>()
                {"Change visibility", "Category", "Region", "Salary", "Education", "Experience", "WorkPlaces", "Skills", "Languages", "HonorsDiploma", "Socials", "Back"};

            EmployerSideMainMenu = new List<string>() {"Your ads", "See Cvs", "Ads Notifications", "See notifications", "Logout"};
            AdsSectionMenu = new List<string>() {"Show", "Add", "Update", "Delete", "Back"};
            VacancyUpdateMenu = new List<string>()
            {
                "Change visibility", "Mail", "Phones", "Category", "Position", "Region", "Salary", "Education",
                "Experience", "Requirements", "Job Description", "Company", "Contact", "Back"
            };

            CvAdsChoices = new List<string>() {"Accept", "Decline", "Back"};
            FilterMenu = new List<string>() {"Select" , "Filter By Category", "Filter By Region", "Filter By Education", "Filter By Experience", "Filter By Salary", "Reset", "Back"};
        }

        public static int Input(int length)
        {
            var logger = new ConsoleLogger();
           
            while (true)
            {
                Console.Write(">> ");

                var status = int.TryParse(Console.ReadLine(), out int choice);

                if (status)
                {
                    if (choice > 0 && choice <= length)
                        return choice;
                    
                    logger.Error($"Input must be between [ 1 - {length} ]!");  
                }
                else
                {
                    logger.Error("Please enter numeric value!");
                }
            }
        }

        public static void PrintMenu(IList<string> options, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.ResetColor();
        }
        public static void Clear()
        {
            Console.WriteLine("Press enter to continue!");
            Console.ReadLine();
            Console.Clear();
        }

        public static DialogResult DisplayMessageBox(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, title, buttons, icon);
        }
    }
}