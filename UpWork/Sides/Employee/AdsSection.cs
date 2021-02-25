using System;
using System.Windows.Forms;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Extensions;
using UpWork.Helpers;

namespace UpWork.Sides.Employee
{
    public static class AdsSection
    {
        public static void Start(Worker worker, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();

            var seeAdsLoop = true;
            while (seeAdsLoop)
            {
#pragma warning disable CS0219 // The variable 'filterUsed' is assigned but its value is never used
                var filterUsed = false;
#pragma warning restore CS0219 // The variable 'filterUsed' is assigned but its value is never used
                var vacancies = db.GetVacancies();

                var loop2 = true;
                while(loop2)
                {
                    Console.Clear();
                    if (!ExceptionHandle.Handle(VacancyHelper.SeeAds, vacancies))
                        break;
                    Console.WriteLine("Vacancy id: ");
                    var vacId = UserHelper.InputGuid();

                    try
                    {
                        var vacancy = VacancyHelper.GetVacancy(vacId, vacancies);

                        var loop3 = true;
                        while (loop3)
                        {
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
                                    Cv cv = null;
                                    while (true)
                                    {
                                        Console.Clear();
                                        if (!ExceptionHandle.Handle(worker.ShowAllCv, true))
                                        {
                                            logger.Info("Please add public Cv!");
                                            break;
                                        }

                                        var cvId = UserHelper.InputGuid();

                                        try
                                        {
                                            cv = CvHelper.GetCv(cvId, worker.Cvs);
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            logger.Error(e.Message);
                                            ConsoleScreen.Clear();
                                        }
                                    }

                                    if (cv != null)
                                    {
                                        vacancy.SendRequest(worker.Guid, cv.Guid);
                                    }
                                }
                            }
                            else if (choice == 2)
                            {
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Ads?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    loop2 = false;
                                break;
                            }
                        }

                        if (loop2)
                            continue;
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        ConsoleScreen.Clear();
                    }

                    if (!loop2 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        break;
                }

                break;
            }
        }
    }
}