using System;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Extensions;
using UpWork.Helpers;
using UpWork.Logger;

namespace UpWork.Sides
{
    public static class CvSection
    {
        public static void Start(Worker worker)
        {
            var logger = new ConsoleLogger();

            var cvSectionLoop = true;

            while (cvSectionLoop)
            {
                ConsoleScreen.PrintMenu(ConsoleScreen.CvSectionMenu, ConsoleColor.DarkGreen);

                var cvSectionChoice = (CvSectionEnum)ConsoleScreen.Input(ConsoleScreen.CvSectionMenu.Count);

                Console.Clear();

                switch (cvSectionChoice)
                {
                    case CvSectionEnum.Show:
                    {
                        ExceptionHandle.Handle(worker.ShowAllCv);
                        ConsoleScreen.Clear();
                        break;
                    }
                    case CvSectionEnum.Add:
                    {
                        var addCvLoop = true;

                        while (addCvLoop)
                        {
                                var newCw = new Cv()
                                {
                                    Speciality = CvHelper.InputData("Speciality"),
                                    School = CvHelper.InputData("School"),
                                    UniScore = CvHelper.InputUniScore(),
                                    HonorsDiploma = CvHelper.InputHonorsDiplomaStatus()
                                };

                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add skill?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Console.WriteLine("Add skill: ");
                                    while (true)
                                    {
                                        newCw.Skills.Add(new Skill()
                                        {
                                            Name = CvHelper.InputData("Skill"),
                                            Level = CvHelper.InputSkillLevel()
                                        });

                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more skill?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            break;
                                    }
                                }


                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add Workplace?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Console.WriteLine("Add workplaces: ");
                                    while (true)
                                    {
                                        newCw.WorkPlaces.Add(new WorkPlaces()
                                        {
                                            Company = CvHelper.InputData("Company"),
                                            Timeline = new Timeline()
                                            {
                                                Start = CvHelper.InputDateTime("Start time(ex mm/dd/yyyy): "),
                                                End = CvHelper.InputDateTime("Start time(ex mm/dd/yyyy): ")
                                            }
                                        });

                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more WorkPlaces?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            break;
                                    }
                                }


                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add general timeline?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Console.WriteLine("Add general timeline: ");
                                    newCw.Timeline = new Timeline()
                                    {
                                        Start = CvHelper.InputDateTime("Start"),
                                        End = CvHelper.InputDateTime("End"),
                                    };
                                }

                                


                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add language?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Console.WriteLine("Add Language");

                                    while (true)
                                    {
                                        newCw.Languages.Add(new Language()
                                        {
                                            Name = CvHelper.InputData("Name"),
                                            Level = CvHelper.InputSkillLevel(),
                                        });

                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more language?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            break;
                                    }
                                }

                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add social?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Console.WriteLine("Add social: ");

                                    while (true)
                                    {
                                        newCw.Socials.Add(new Social()
                                        {
                                            Name = CvHelper.InputData("Name"),
                                            Link = CvHelper.InputLink()
                                        });

                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more social?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            break;
                                    }
                                }
                                

                                worker.Cvs.Add(newCw);

                                logger.Info("Cv added!");

                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more Cv?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                {
                                    addCvLoop = false;
                                }

                                Console.Clear();
                        }
                        break;
                    }
                    case CvSectionEnum.Update:
                    {
                        if (ExceptionHandle.Handle(worker.ShowAllCv))
                        {

                        }
                        else
                        {
                            ConsoleScreen.Clear();
                        }
                        break;
                    }
                    case CvSectionEnum.Delete:
                    {
                        if (ExceptionHandle.Handle(worker.ShowAllCv))
                        {
                            var id = UserHelper.InputGuid();

                            if (ExceptionHandle.Handle(worker.DeleteCv, id))
                            {
                                logger.Info("Cv deleted!");
                            }
                            ConsoleScreen.Clear();
                        }
                        else
                        {
                            ConsoleScreen.Clear();
                        }
                        break;
                    }
                    case CvSectionEnum.Back:
                    {
                        cvSectionLoop = false;
                        break;
                    }
                }
            }
        }
    }
}