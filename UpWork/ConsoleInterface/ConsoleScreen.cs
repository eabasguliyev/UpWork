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

        static ConsoleScreen()
        {
            UserAccess = new List<string>() {"Login", "Register", "Exit"};
            UserType = new List<string>() {"Worker", "Employer"};
            WorkerSideMainMenu = new List<string>() {"See ads", "Your CV", "Logout"};
            CvSectionMenu = new List<string>() {"Show", "Add", "Update", "Delete", "Back"};
            SkillLevels = new List<string>() {"Beginner", "Intermediate", "Advanced"};
            CvUpdateMenu = new List<string>()
                {"Speciality", "School", "UniScore", "WorkPlaces", "Timeline", "Skills", "Languages", "HonorsDiploma", "Socials", "Back"};


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