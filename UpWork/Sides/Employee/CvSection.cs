using System;
using System.Windows.Forms;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Extensions;
using UpWork.Helpers;
using UpWork.Logger;

namespace UpWork.Sides.Employee
{
    public static class CvSection
    {
        public static void Start(Worker worker)
        {
            var logger = new ConsoleLogger();

            var cvSectionLoop = true;

            while (cvSectionLoop)
            {
                Console.Clear();
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
                            worker.Cvs.Add(CreateNewCv());

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
                        while (true)
                        {
                            Console.Clear();
                            if(!ExceptionHandle.Handle(worker.ShowAllCv))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var id = UserHelper.InputGuid();

                            try
                            {
                                var cv = worker.GetCv(id) as Cv;

                                CvUpdateSideStart(cv);
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to update another Cv?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    break;
                                continue;
                            }
                            catch (Exception e)
                            {
                                logger.Error(e.Message);
                            }

                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }
                        break;
                    }
                    case CvSectionEnum.Delete:
                    {
                        while (true)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(worker.ShowAllCv))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var id = UserHelper.InputGuid();

                            if (ExceptionHandle.Handle(worker.DeleteCv, id))
                            {
                                logger.Info("Cv deleted!");

                                if (worker.Cvs.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete another Cv?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    break;
                                continue;
                            }

                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
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

        private static Cv CreateNewCv()
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
                    newCw.WorkPlaces.Add(new WorkPlace()
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

            return newCw;
        }
        private static void CvUpdateSideStart(Cv cv)
        {
            var logger = new ConsoleLogger();
            var cvUpdateLoop = true;

            while (cvUpdateLoop)
            {
                Console.Clear();

                Console.WriteLine(cv);
                Console.WriteLine();

                ConsoleScreen.PrintMenu(ConsoleScreen.CvUpdateMenu, ConsoleColor.Blue);

                var updateChoice =
                    (CvUpdateChoices)ConsoleScreen.Input(ConsoleScreen.CvUpdateMenu.Count);

                switch (updateChoice)
                {
                    case CvUpdateChoices.Speciality:
                        {
                            cv.Speciality = CvHelper.InputData("Speciality");

                            logger.Info("Speciality updated!");
                            break;
                        }
                    case CvUpdateChoices.School:
                        {
                            cv.School = CvHelper.InputData("School");

                            logger.Info("School updated");
                            break;
                        }
                    case CvUpdateChoices.UniScore:
                        {
                            cv.UniScore = CvHelper.InputUniScore();

                            logger.Info("Uni score updated!");
                            break;
                        }
                    case CvUpdateChoices.WorkPlaces:
                        {
                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add workplace or delete?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Console.WriteLine("Add workplaces: ");
                                while (true)
                                {
                                    cv.WorkPlaces.Add(new WorkPlace()
                                    {
                                        Company = CvHelper.InputData("Company"),
                                        Timeline = new Timeline()
                                        {
                                            Start = CvHelper.InputDateTime("Start time(ex mm/dd/yyyy): "),
                                            End = CvHelper.InputDateTime("End time(ex mm/dd/yyyy): ")
                                        }
                                    });

                                    logger.Info("Workplace added");
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more WorkPlaces?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            else
                            {
                                if (cv.WorkPlaces.Count > 0)
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Delete workplace: ");
                                        foreach (var workPlace in cv.WorkPlaces)
                                        {
                                            Console.WriteLine(workPlace);
                                        }

                                        var workplaceId = UserHelper.InputGuid();

                                        if (ExceptionHandle.Handle(cv.DeleteWorkplace, workplaceId))
                                        {
                                            logger.Info("Workplace deleted");


                                            if (cv.WorkPlaces.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete more WorkPlaces?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case CvUpdateChoices.Timeline:
                        {
                            var newTimeLine = new Timeline()
                            {
                                Start = CvHelper.InputDateTime("Start: "),
                                End = CvHelper.InputDateTime("End: "),
                            };
                            cv.Timeline = newTimeLine;
                            logger.Info("Timeline updated!");
                            break;
                        }
                    case CvUpdateChoices.Skills:
                        {
                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add skill or delete?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Console.WriteLine("Add skill: ");
                                while (true)
                                {
                                    cv.Skills.Add(new Skill()
                                    {
                                        Name = CvHelper.InputData("Name"),
                                        Level = CvHelper.InputSkillLevel(),
                                    });

                                    logger.Info("Skill added");
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more Skills?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            else
                            {
                                if (cv.Skills.Count > 0)
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Delete skill: ");
                                        foreach (var skill in cv.Skills)
                                        {
                                            Console.WriteLine(skill);
                                        }

                                        var skillId = UserHelper.InputGuid();

                                        if (ExceptionHandle.Handle(cv.DeleteSkill, skillId))
                                        {
                                            logger.Info("Skill deleted");


                                            if (cv.Skills.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete more Skill?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case CvUpdateChoices.Languages:
                        {
                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add language or delete?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Console.WriteLine("Add languages: ");
                                while (true)
                                {
                                    cv.Languages.Add(new Language()
                                    {
                                        Name = CvHelper.InputData("Name"),
                                        Level = CvHelper.InputSkillLevel(),
                                    });

                                    logger.Info("Language added");
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more Languages?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            else
                            {
                                if (cv.Languages.Count > 0)
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Delete language: ");
                                        foreach (var language in cv.Languages)
                                        {
                                            Console.WriteLine(language);
                                        }

                                        var skillId = UserHelper.InputGuid();

                                        if (ExceptionHandle.Handle(cv.DeleteLanguage, skillId))
                                        {
                                            logger.Info("Language deleted");


                                            if (cv.Languages.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete more Language?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case CvUpdateChoices.HonorsDiploma:
                        {
                            cv.HonorsDiploma = CvHelper.InputHonorsDiplomaStatus();

                            logger.Info("Honors diploma updated!");
                            break;
                        }
                    case CvUpdateChoices.Socials:
                        {
                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add Social or delete?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Console.WriteLine("Add socials: ");
                                while (true)
                                {
                                    cv.Socials.Add(new Social()
                                    {
                                        Name = CvHelper.InputData("Name"),
                                        Link = CvHelper.InputLink()
                                    });

                                    logger.Info("Social added");
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more Socials?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            else
                            {
                                if (cv.Socials.Count > 0)
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Delete social: ");
                                        foreach (var social in cv.Socials)
                                        {
                                            Console.WriteLine(social);
                                        }

                                        var socialId = UserHelper.InputGuid();

                                        if (ExceptionHandle.Handle(cv.DeleteSocial, socialId))
                                        {
                                            logger.Info("Social deleted");


                                            if (cv.Socials.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete more Social?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case CvUpdateChoices.Back:
                        {
                            cvUpdateLoop = false;
                            break;
                        }
                }
                ConsoleScreen.Clear();
            }
        }
    }
}