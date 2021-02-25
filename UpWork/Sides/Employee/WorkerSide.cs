using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Extensions;
using UpWork.Helpers;
using UpWork.Logger;

namespace UpWork.Sides.Employee
{
    public static class WorkerSide
    {
        public static void Start(Worker worker, Database.Database db)
        {
            var workerSideMainLoop = true;

            while (workerSideMainLoop)
            {
                Console.Clear();

                ConsoleScreen.PrintMenu(ConsoleScreen.WorkerSideMainMenu, ConsoleColor.DarkGreen);

                var mainChoice = (WorkerSideMainMenu) ConsoleScreen.Input(ConsoleScreen.WorkerSideMainMenu.Count);

                Console.Clear();
                
                switch (mainChoice)
                {
                    case WorkerSideMainMenu.SEEADS:
                    {
                        AdsSection.Start(worker, db);
                        break;
                    }
                    case WorkerSideMainMenu.YOURCV:
                    {
                        CvSection.Start(worker);
                        break;
                    }
                    case WorkerSideMainMenu.CvNotifications:
                    {

                        break;
                    }
                    case WorkerSideMainMenu.SeeNotifications:
                    {
                        while (true)
                        {
                            if (!ExceptionHandle.Handle(worker.ShowShortNotfInfo))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var notificationId = UserHelper.InputGuid();


                            var notification = worker.Notifications.SingleOrDefault(n => n.Guid == notificationId);

                            if (notification == null)
                            {
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    break;
                                continue;
                            }

                            Console.Clear();
                            Console.WriteLine(notification);

                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other notifications?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }
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