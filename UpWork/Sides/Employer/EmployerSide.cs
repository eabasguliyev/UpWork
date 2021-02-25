using System;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Extensions;
using UpWork.Helpers;
using UpWork.Logger;
using UpWork.NotificationSender;
using Publisher = System.Security.Policy.Publisher;

namespace UpWork.Sides.Employer
{
    public static class EmployerSide
    {
        public static void Start(Entities.Employer employer, Database.Database db)
        {
            var logger = new ConsoleLogger();
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
                        CvSection.Start(employer, db);
                        break;
                    }
                    case EmployerSideMainMenu.AdsNotifications:
                    {
                        while (true)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(employer.ShowAllAdsWithRequestCount))
                                break;

                            var vacancyId = UserHelper.InputGuid();

                            Vacancy vacancy = null;
                            try
                            {
                                vacancy = employer.GetVacancy(vacancyId);

                                if (vacancy.RequestsFromWorkers.Count != 0)
                                {
                                    var cvs = db.GetAllCvFromRequests(vacancy.RequestsFromWorkers);

                                    while (true)
                                    {
                                        Console.Clear();
                                        if (!ExceptionHandle.Handle(EmployerHelper.ShowCvs, cvs))
                                            break;

                                        var cvId = UserHelper.InputGuid();


                                        var cv = cvs.SingleOrDefault(c => c.Guid == cvId);

                                        if (cv == null)
                                        {
                                            logger.Error($"There is no cv associated this id -> {cvId}");
                                            ConsoleScreen.Clear();
                                            continue;
                                        }

                                        var worker = DatabaseHelper.GetWorker(vacancy.RequestsFromWorkers
                                            .SingleOrDefault(r => r.Value == cv.Guid).Key, db.GetWorkers());

                                        Console.Clear();

                                        Console.WriteLine(cv);

                                        ConsoleScreen.PrintMenu(ConsoleScreen.CvAdsChoices, ConsoleColor.DarkGreen);

                                        var choice =
                                            (CvAdsChoices) ConsoleScreen.Input(ConsoleScreen.CvAdsChoices.Count);


                                        if (choice == CvAdsChoices.Back)
                                            break;


                                        switch (choice)
                                        {
                                            case CvAdsChoices.Accept:
                                            {
                                                vacancy.RemoveRequest(worker.Guid);

                                                NotificationSender.Publisher.OnSend(worker, new Notification(){Title="Your apply result", Message = $"Congratilations. Your cv accepted.\n Vacancy info:\n{vacancy}"});
                                                logger.Info("Accepted.");
                                                break;
                                            }
                                            case CvAdsChoices.Decline:
                                            {
                                                vacancy.RemoveRequest(worker.Guid);
                                                NotificationSender.Publisher.OnSend(worker, new Notification() { Title = "Your apply result", Message = $"We are sorry! Your cv declined.\n Vacancy info:\n{vacancy}" });
                                                logger.Info("Declined.");
                                                break;
                                            }
                                        }

                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Cvs?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            break;
                                    }
                                }
                                else
                                {
                                    logger.Error("There is no request!");
                                }
                            }
                            catch (Exception e)
                            { 
                                logger.Error(e.Message);   
                            }

                            if (vacancy == null && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }

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