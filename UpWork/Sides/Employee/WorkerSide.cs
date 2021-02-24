using System;
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
                        var seeAdsLoop = true;
                        while (seeAdsLoop)
                        {
                            var filterUsed = false;
                            var vacancies = db.GetVacancies();

                            if (ExceptionHandle.Handle(worker.SeeAds, vacancies))
                            {
                                Console.WriteLine("Vacancy id: ");
                                var vacId = UserHelper.InputGuid();

                                while (true)
                                {
                                    try
                                    {
                                        var vacancy = VacancyHelper.GetVacancy(vacId, vacancies);
                                        var requestFromWorker = vacancy.CheckWorkerRequest(worker.Guid);
                                        Console.Clear();
                                        Console.WriteLine(vacancy);

                                        Console.WriteLine();
                                        Console.WriteLine($"1. {(requestFromWorker ? "Cancel" : "Request")}"); ;
                                        Console.WriteLine("2. Back");
                                        var choice = ConsoleScreen.Input(2);

                                        if (choice == 1)
                                        {
                                            if (requestFromWorker)
                                            {
                                                vacancy.CancelRequest(worker.Guid);
                                            }
                                            else
                                            {
                                                vacancy.SendRequest(worker.Guid);
                                            }
                                        }
                                        else if (choice == 2)
                                        {
                                            break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        logger.Error(e.Message);
                                    }
                                }
                            }
                        }
                        break;
                    }
                    case WorkerSideMainMenu.YOURCV:
                    {
                        CvSection.Start(worker);
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