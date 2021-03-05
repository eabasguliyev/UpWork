using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.DataFilter;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Helpers;

namespace UpWork.Sides.Employee
{
    public static class AdsSection
    {
        public static void Start(Worker worker, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();

            var seeAdsLoop = true;

            var mainVacancies = db.GetVacancies();
            IList<Vacancy> vacancies = mainVacancies;

            while (seeAdsLoop)
            {
                Console.Clear();
                
                ExceptionHandle.Handle(VacancyHelper.SeeAds, vacancies);

                ConsoleScreen.PrintMenu(ConsoleScreen.FilterMenu, ConsoleColor.Blue);

                var filterMenuChoice = (FilterMenuEnum)ConsoleScreen.Input(ConsoleScreen.FilterMenu.Count);

                switch (filterMenuChoice)
                {
                    case FilterMenuEnum.Select:
                    {
                        var loop2 = true;
                        while (loop2)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(VacancyHelper.SeeAds, vacancies))
                                break;
                            Console.WriteLine("Vacancy id: ");
                            var vacId = UserHelper.InputGuid();

                            try
                            {
                                var vacancy = VacancyHelper.GetVacancy(vacId, vacancies);

                                while (true)
                                {
                                    var requestFromWorker = vacancy.CheckWorkerRequest(worker.Guid);
                                    Console.Clear();
                                    Console.WriteLine(vacancy++); // increase vacancy view count and print
                                    Database.Database.Changes = true;

                                    Console.WriteLine();
                                    Console.WriteLine($"1. {(requestFromWorker ? "Cancel" : "Request")}"); ;
                                    Console.WriteLine("2. Back");
                                    var choice = ConsoleScreen.Input(2);

                                    if (choice == 1)
                                    {
                                        if (requestFromWorker)
                                        {
                                            vacancy.RemoveRequest(worker.Guid);
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
                                                    ConsoleScreen.Clear();
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
                    case FilterMenuEnum.ByCategory:
                    {
                        Console.Clear();

                        vacancies = VacancyFilter.FilterByCategory(UserHelper.InputCategory(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByEducation:
                    {
                        Console.Clear();

                        vacancies = VacancyFilter.FilterByEducation(UserHelper.InputEducation(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByExperience:
                    {
                        Console.Clear();

                        vacancies = VacancyFilter.FilterByExperience(UserHelper.InputExperience(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByRegion:
                    {
                        Console.Clear();

                        vacancies = VacancyFilter.FilterByRegion(UserHelper.InputRegion(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.BySalary:
                    {
                        Console.Clear();
                        var input = UserHelper.InputSalary();
                        var salary = UserHelper.ParseSalary(input);
                        
                        vacancies = VacancyFilter.FilterBySalary(salary, vacancies);
                        break;
                    }
                    case FilterMenuEnum.Reset:
                    {
                        vacancies = mainVacancies;
                        break;
                    }
                    case FilterMenuEnum.Back:
                    {
                        seeAdsLoop = false;
                        break;
                    }
                }
            }
        }
    }
}