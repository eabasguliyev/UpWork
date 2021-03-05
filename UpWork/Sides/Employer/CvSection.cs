using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using UpWork.ConsoleInterface;
using UpWork.DataFilter;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Helpers;

namespace UpWork.Sides.Employer
{
    public static class CvSection
    {
        public static void Start(Entities.Employer employer, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();

            var seeCvsLoop = true;


            var mainCvs = db.GetCvs();

            IList<Cv> cvs = mainCvs;


            while (seeCvsLoop)
            {
                Console.Clear();

                ExceptionHandle.Handle(CvHelper.SeeCvs, cvs);

                ConsoleScreen.PrintMenu(ConsoleScreen.FilterMenu, ConsoleColor.Blue);

                var filterMenuChoice = (FilterMenuEnum)ConsoleScreen.Input(ConsoleScreen.FilterMenu.Count);

                switch (filterMenuChoice)
                {
                    case FilterMenuEnum.Select:
                    {
                        var loop3 = true;
                        while (loop3)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(CvHelper.SeeCvs, cvs))
                                break;
                            Console.WriteLine("Cv id: ");

                            var cvId = UserHelper.InputGuid();

                            try
                            {
                                var cv = CvHelper.GetCv(cvId, cvs);

                                Console.Clear();

                                while (true)
                                {
                                    var requestFromEmployer = cv.CheckEmployerRequest(employer.Guid);

                                    Console.Clear();

                                    Console.WriteLine(cv++); // increase view count and print.

                                    Database.Database.Changes = true;

                                    Console.WriteLine();

                                    Console.WriteLine($"1. {(requestFromEmployer ? "Cancel" : "Request")}");

                                    Console.WriteLine("2. Back");

                                    var choice = ConsoleScreen.Input(2);

                                    if (choice == 1)
                                    {
                                        if (requestFromEmployer)
                                        {
                                            cv.RemoveRequest(employer.Guid);
                                        }
                                        else
                                        {
                                            Vacancy vacancy = null;

                                            while (true)
                                            {
                                                Console.Clear();
                                                if (!ExceptionHandle.Handle(employer.ShowAllAds, true))
                                                {
                                                    logger.Info("Please add public Vacancy!");
                                                    ConsoleScreen.Clear();
                                                    break;
                                                }

                                                var vacancyId = UserHelper.InputGuid();


                                                try
                                                {
                                                    vacancy = VacancyHelper.GetVacancy(vacancyId, employer.Vacancies);
                                                    break;
                                                }
                                                catch (Exception e)
                                                {
                                                    logger.Error(e.Message);
                                                    ConsoleScreen.Clear();
                                                }
                                            }

                                            if (vacancy != null)
                                            {
                                                cv.SendRequest(employer.Guid, vacancy.Guid);
                                            }
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
                        }
                        break;
                    }
                    case FilterMenuEnum.ByCategory:
                    {
                        Console.Clear();

                        cvs = CvFilter.FilterByCategory(UserHelper.InputCategory(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByEducation:
                    {
                        Console.Clear();

                        cvs = CvFilter.FilterByEducation(UserHelper.InputEducation(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByExperience:
                    {
                        Console.Clear();

                        cvs = CvFilter.FilterByExperience(UserHelper.InputExperience(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByRegion:
                    {
                        Console.Clear();

                        cvs = CvFilter.FilterByRegion(UserHelper.InputRegion(), cvs);
                        break;
                    }
                    case FilterMenuEnum.BySalary:
                    {
                        Console.Clear();

                        var input = UserHelper.InputSalary();
                        var salary = UserHelper.ParseSalary(input);
                        cvs = CvFilter.FilterBySalary(salary, cvs);
                        break;
                    }
                    case FilterMenuEnum.Reset:
                    {
                        cvs = mainCvs;
                        break;
                    }
                    case FilterMenuEnum.Back:
                    {
                        seeCvsLoop = false;
                        break;
                    }
                }
            }
        }
    }
}