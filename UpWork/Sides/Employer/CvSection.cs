using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Extensions;
using UpWork.Helpers;

namespace UpWork.Sides.Employer
{
    public static class CvSection
    {
        public static void Start(Entities.Employer employer, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();

            var seeWorkersLoop = true;

            while (seeWorkersLoop)
            {
#pragma warning disable CS0219 // The variable 'filterUsed' is assigned but its value is never used
                var filterUsed = false;
#pragma warning restore CS0219 // The variable 'filterUsed' is assigned but its value is never used

                var workers = db.GetWorkers();

                var loop2 = true;
                while (loop2)
                {
                    Console.Clear();
                    if (!ExceptionHandle.Handle(DatabaseHelper.ShowWorkers, workers))
                        break;
                    try
                    {
                        Console.WriteLine("Worker id: ");

                        var workerId = UserHelper.InputGuid();

                        var worker = DatabaseHelper.GetWorker(workerId, workers);

                        Console.Clear();

                        var loop3 = true;
                        while (loop3)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(worker.ShowAllCv, true))
                                break;
                            Console.WriteLine("Cv id: ");

                            var cvId = UserHelper.InputGuid();

                            try
                            {
                                var cv = CvHelper.GetCv(cvId, worker.Cvs);

                                Console.Clear();

                                while (true)
                                {
                                    var requestFromEmployer = cv.CheckEmployerRequest(employer.Guid);

                                    Console.Clear();

                                    Console.WriteLine(cv);

                                    Console.WriteLine();

                                    Console.WriteLine($"1. {(requestFromEmployer ? "Cancel" : "Request")}");

                                    Console.WriteLine("2. Back");

                                    var choice = ConsoleScreen.Input(2);

                                    if (choice == 1)
                                    {
                                        if (requestFromEmployer)
                                        {
                                            cv.CancelRequest(employer.Guid);
                                        }
                                        else
                                        {
                                            cv.SendRequest(employer.Guid);
                                        }
                                    }
                                    else if (choice == 2)
                                    {
                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Cvs?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            loop3 = false;
                                        break;
                                    }
                                }

                                if (loop3)
                                    continue;
                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                            }

                            if (!loop3)
                            {
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other workers?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    loop2 = false;
                                else
                                    loop3 = true;
                                    break;
                            }
                            else if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other workers?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    loop2 = false;

                                break;
                            }
                        }

                        if (loop3)
                            continue;
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                    }

                    if (!loop2 ||ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        break;
                }
                break;
            }
        }
    }
}