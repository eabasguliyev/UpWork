using System;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Logger;

namespace UpWork.Sides
{
    public static class WorkerSide
    {
        public static void Start(Worker user, Database.Database db)
        {
            var logger = new ConsoleLogger();

            var workerSideMainLoop = true;

            while (workerSideMainLoop)
            {
                ConsoleScreen.PrintMenu(ConsoleScreen.WorkerSideMainMenu, ConsoleColor.DarkGreen);

                var mainChoice = (WorkerSideMainMenu) ConsoleScreen.Input(ConsoleScreen.WorkerSideMainMenu.Count);

                Console.Clear();
                
                switch (mainChoice)
                {
                    case WorkerSideMainMenu.SEEADS:
                    {
                        break;
                    }
                    case WorkerSideMainMenu.YOURCV:
                    {
                        CvSection.Start(user);
                        break;
                    }
                    case WorkerSideMainMenu.Logout:
                    {
                        workerSideMainLoop = false;
                        break;
                    }
                }
            }
        }
    }
}