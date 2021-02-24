using System;
using UpWork.ConsoleInterface;
using UpWork.Enums;
using UpWork.Logger;

namespace UpWork.Sides.Employer
{
    public static class EmployerSide
    {
        public static void Start(Entities.Employer employer, Database.Database db)
        {
            var employerSideMainLoop = true;

            while (employerSideMainLoop)
            {
                Console.Clear();

                ConsoleScreen.PrintMenu(ConsoleScreen.EmployerSideMainMenu, ConsoleColor.DarkGreen);

                var employerSideChoice =
                    (EmployerSideMainMenu) ConsoleScreen.Input(ConsoleScreen.EmployerSideMainMenu.Count);

                Console.Clear();

                switch (employerSideChoice)
                {
                    case EmployerSideMainMenu.YourAds:
                    {
                        AdsSection.Start(employer);
                        break;
                    }
                    case EmployerSideMainMenu.SeeWorkers:
                    {
                        break;
                    }
                    case EmployerSideMainMenu.SeeNotifications:
                    {
                        break;
                    }
                    case EmployerSideMainMenu.Logout:
                    {
                        employerSideMainLoop = false;
                        break;
                    }
                }
            }
        }
    }
}