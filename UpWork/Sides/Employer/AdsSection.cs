using System;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;

using UpWork.Helpers;
using UpWork.Logger;

namespace UpWork.Sides.Employer
{
    public static class AdsSection
    {
        public static void Start(Entities.Employer employer)
        {
            var logger = new ConsoleLogger();
            var adsSectionLoop = true;

            while (adsSectionLoop)
            {
                Console.Clear();
                ConsoleScreen.PrintMenu(ConsoleScreen.AdsSectionMenu, ConsoleColor.DarkGreen);

                var adsSectionChoice = (AdsSectionEnum)ConsoleScreen.Input(ConsoleScreen.AdsSectionMenu.Count);
                
                Console.Clear();
                
                switch (adsSectionChoice)
                {
                    case AdsSectionEnum.Show:
                    {
                        ExceptionHandle.Handle(employer.ShowAllAds, false);
                        ConsoleScreen.Clear();
                        break;
                    }
                    case AdsSectionEnum.Add:
                    {
                        var addCvLoop = true;

                        while (addCvLoop)
                        {
                            employer.Vacancies.Add(CreateNewVacancy());
                            
                            logger.Info("Vacancy added!");

                            if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more Vacancy?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                addCvLoop = false;
                            }

                            Console.Clear();
                        }
                        Database.Database.Changes = true;
                            break;
                    }
                    case AdsSectionEnum.Update:
                    {
                        while (true)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(employer.ShowAllAds, false))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var id = UserHelper.InputGuid();

                            try
                            {
                                var vacancy = employer.GetVacancy(id);

                                VacancyUpdateSideStart(vacancy);
                                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to update another Vacancy?",
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
                    case AdsSectionEnum.Delete:
                    {
                        while (true)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(employer.ShowAllAds, false))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var vacancyId = UserHelper.InputGuid();

                            if (ExceptionHandle.Handle(employer.DeleteVacancy, vacancyId))
                            {
                                logger.Info("Cv deleted!");
                                Database.Database.Changes = true;
                                
                                if (employer.Vacancies.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete another Vacancy?",
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
                    case AdsSectionEnum.Back:
                    {
                        adsSectionLoop = false;
                        break;
                    }
                }
            }
        }

        private static void VacancyUpdateSideStart(Vacancy vacancy)
        {
            var logger = new ConsoleLogger();

            while (true)
            {
                Console.Clear();

                Console.WriteLine(vacancy);

                Console.WriteLine();

                ConsoleScreen.PrintMenu(ConsoleScreen.VacancyUpdateMenu, ConsoleColor.DarkGreen);

                var updateChoice = (VacancyUpdateChoices) ConsoleScreen.Input(ConsoleScreen.VacancyUpdateMenu.Count);

                if (updateChoice == VacancyUpdateChoices.Back)
                {
                    break;
                }

                switch (updateChoice)
                {
                    case VacancyUpdateChoices.ChangeVisibility:
                    {
                        vacancy.IsPublic = !vacancy.IsPublic;
                        logger.Info($"Visibility changed to {(vacancy.IsPublic ? "Public" : "Private")}");
                        break;
                    }
                    case VacancyUpdateChoices.Mail:
                    {

                        while (true)
                        {
                            var mail = VacancyHelper.InputData("Mail");
                            if (ExceptionHandle.Handle(UserHelper.ValidateMail, mail))
                            {
                                vacancy.Contact.Mail = mail;
                                break;
                            }
                        }

                        logger.Info("Mail updated!");
                            break;
                    }
                    case VacancyUpdateChoices.Phones:
                    {
                        if (vacancy.Contact.Phones.Count != 3 && ConsoleScreen.DisplayMessageBox("Info", "Do you want to add phone number or delete?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Console.WriteLine("Add phone number: ");

                            var maxPhoneNumbers = 3 - vacancy.Contact.Phones.Count;
                            while (maxPhoneNumbers > 0)
                            {
                                var phone = VacancyHelper.InputData("Phone");

                                if (ExceptionHandle.Handle(UserHelper.ValidatePhone, phone))
                                {
                                    vacancy.Contact.Phones.Add(phone);
                                    maxPhoneNumbers--;

                                    if (maxPhoneNumbers == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more phone number?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                    continue;
                                }
                                if (maxPhoneNumbers != 3 && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    break;
                            }
                        }
                        else
                        {
                            if (vacancy.Contact.Phones.Count > 0)
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Delete phone number: ");
                                    
                                    for (var i = 0; i < vacancy.Contact.Phones.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {vacancy.Contact.Phones[i]}");   
                                    }

                                    var phoneIndex = ConsoleScreen.Input(vacancy.Contact.Phones.Count) - 1;

                                    vacancy.Contact.Phones.RemoveAt(phoneIndex);

                                    logger.Info("Phone number deleted");


                                    if (vacancy.Contact.Phones.Count == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to delete more phone number?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                        }

                        break;
                    }
                    case VacancyUpdateChoices.Category:
                    {
                        vacancy.Ad.Category = UserHelper.InputCategory();

                        logger.Info("Category updated");
                        break;
                    }
                    case VacancyUpdateChoices.Position:
                    {
                        vacancy.Ad.Position = VacancyHelper.InputData("Position");

                        logger.Info("Position updated");
                        break;
                    }
                    case VacancyUpdateChoices.Region:
                    {
                        vacancy.Ad.Region = UserHelper.InputRegion();

                        logger.Info("Region updated");
                        break;
                    }
                    case VacancyUpdateChoices.Salary:
                    {
                        vacancy.Ad.SalaryRange = UserHelper.InputSalaryRange();

                        logger.Info("Salary updated");
                        break;
                    }
                    case VacancyUpdateChoices.Education:
                    {
                        vacancy.Ad.Education = UserHelper.InputEducation();
                        
                        logger.Info("Education updated");
                        break;
                    }
                    case VacancyUpdateChoices.Experience:
                    {
                        vacancy.Ad.Experience = UserHelper.InputExperience();

                        logger.Info("Experience updated");
                        break;
                    }
                    case VacancyUpdateChoices.Requirements:
                    {
                        vacancy.Ad.Requirements = VacancyHelper.InputData("Requirements");

                        logger.Info("Requirements updated");
                        break;
                    }
                    case VacancyUpdateChoices.JobDescription:
                    {
                        vacancy.Ad.JobDescription = VacancyHelper.InputData("Job Description");

                        logger.Info("Job Description updated");
                        break;
                    }
                    case VacancyUpdateChoices.Company:
                    {
                        vacancy.Ad.Company = VacancyHelper.InputData("Company");

                        logger.Info("Company updated");
                        break;
                    }
                    case VacancyUpdateChoices.Contact:
                    {
                        vacancy.Ad.Contact = VacancyHelper.InputData("Contact");

                        logger.Info("Contact updated");
                        break;
                    }
                }
                Database.Database.Changes = true;
                ConsoleScreen.Clear();
            }
        }

        private static Vacancy CreateNewVacancy()
        {
            var newVacancy = new Vacancy {Contact = new Contact()};

            // contact initialize

            while (true)
            {
                var mail = VacancyHelper.InputData("Mail");
                if (ExceptionHandle.Handle(UserHelper.ValidateMail, mail))
                {
                    newVacancy.Contact.Mail = mail;
                    break;
                }
            }

            var maxPhoneNumbers = 3;
            while (maxPhoneNumbers > 0)
            {
                var phone = VacancyHelper.InputData("Phone");

                if (ExceptionHandle.Handle(UserHelper.ValidatePhone, phone))
                {
                    newVacancy.Contact.Phones.Add(phone);
                    maxPhoneNumbers--;

                    if (maxPhoneNumbers == 0 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more phone number?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        break;
                    continue;
                }
                if (maxPhoneNumbers != 3 && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    break;
            }

            // add initialize

            newVacancy.Ad = new Advert();


            newVacancy.Ad.Category = UserHelper.InputCategory();

            Console.Clear();
            
            newVacancy.Ad.Position = VacancyHelper.InputData("Position");
            
            newVacancy.Ad.Region = UserHelper.InputRegion();



            newVacancy.Ad.Education = UserHelper.InputEducation();



            newVacancy.Ad.Experience = UserHelper.InputExperience();

            Console.Clear();
            
            newVacancy.Ad.Requirements = VacancyHelper.InputData("Requirements");

            Console.Clear();

            newVacancy.Ad.JobDescription = VacancyHelper.InputData("Job Description");

            Console.Clear();

            newVacancy.Ad.Company = VacancyHelper.InputData("Company");

            Console.Clear();

            newVacancy.Ad.Contact = VacancyHelper.InputData("Contact");

            newVacancy.Ad.SalaryRange = UserHelper.InputSalaryRange();
            return newVacancy;
        }
    }
}