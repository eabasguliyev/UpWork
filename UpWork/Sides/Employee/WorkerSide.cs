using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;

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
                Console.Title = $"Worker: {worker.Name}";
                if (Database.Database.Changes)
                {
                    Data.Data.WriteToJson(db);
                    Database.Database.Changes = false;
                }

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
                        while (true)
                        {
                            Console.Clear();

                            if (!ExceptionHandle.Handle(worker.ShowAllCvWithRequestCount))
                                break;

                            var cvId = UserHelper.InputGuid();

                            Cv cv = null;

                            try
                            {
                                cv = worker.GetCv(cvId);

                                if (cv.RequestFromEmployers.Count == 0)
                                {
                                    logger.Error("There is no request!");
                                    ConsoleScreen.Clear();
                                }

                                var vacancies = db.GetAllVacanciesFromRequests(cv.RequestFromEmployers);

                                while (true)
                                {
                                    Console.Clear();

                                    if (!ExceptionHandle.Handle(WorkerHelper.ShowVacancies, vacancies))
                                        break;

                                    var vacancyId = UserHelper.InputGuid();

                                    var vacancy = vacancies.SingleOrDefault(v => v.Guid == vacancyId);

                                    if (vacancy == null)
                                    {
                                        logger.Error($"There is no vacancy associated this id -> {vacancyId}");
                                        ConsoleScreen.Clear();
                                        continue;
                                    }

                                    var employer =
                                        DatabaseHelper.GetUser(
                                            cv.RequestFromEmployers.SingleOrDefault(r => r.Value == vacancy.Guid).Key,
                                            db.Users) as Entities.Employer;


                                    Console.Clear();

                                    Console.WriteLine(vacancy);

                                    ConsoleScreen.PrintMenu(ConsoleScreen.CvAdsChoices, ConsoleColor.DarkGreen);

                                    var choice =
                                        (CvAdsChoices)ConsoleScreen.Input(ConsoleScreen.CvAdsChoices.Count);


                                    if (choice == CvAdsChoices.Back)
                                        break;

                                    Database.Database.Changes = true;
                                    switch (choice)
                                    {
                                        case CvAdsChoices.Accept:
                                            {
                                                cv.RemoveRequest(employer.Guid);

                                                NotificationSender.Publisher.OnSend(employer, new Notification() { Title = "From worker", Message = $"Congratilations. Your request accepted.\n Cv info:\n{cv}" });
                                                logger.Info("Accepted.");
                                                break;
                                            }
                                        case CvAdsChoices.Decline:
                                            {
                                                cv.RemoveRequest(employer.Guid);
                                                NotificationSender.Publisher.OnSend(employer, new Notification() { Title = "From worker", Message = $"We are sorry! Your request declined.\n Cv info:\n{cv}" });
                                                logger.Info("Declined.");
                                                break;
                                            }
                                    }

                                    Database.Database.Changes = true;
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Vacancies?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                            }

                            if (cv == null && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }
                        break;
                    }
                    case WorkerSideMainMenu.SeeNotifications:
                    {
                        NotificationSide.Start(worker);
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