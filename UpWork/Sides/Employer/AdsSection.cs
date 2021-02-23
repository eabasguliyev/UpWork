using System;
using System.Windows.Forms;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Extensions;
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
                        ExceptionHandle.Handle(employer.ShowAllAds);
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
                        break;
                    }
                    case AdsSectionEnum.Update:
                    {
                        break;
                    }
                    case AdsSectionEnum.Delete:
                    {
                        while (true)
                        {
                            Console.Clear();
                            if (!ExceptionHandle.Handle(employer.ShowAllAds))
                            {
                                ConsoleScreen.Clear();
                                break;
                            }

                            var vacancyId = UserHelper.InputGuid();

                            if (ExceptionHandle.Handle(employer.DeleteVacancy, vacancyId))
                            {
                                logger.Info("Cv deleted!");

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

                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to add more phone number?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        break;
                    continue;
                }
                if (maxPhoneNumbers != 3 && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    break;
            }

            // add initialize

            newVacancy.Ad = new Advert()
            {
                Category = VacancyHelper.InputData("Category"),
                Position = VacancyHelper.InputData("Position"),
                Region = VacancyHelper.InputData("Region"),
                Education = VacancyHelper.InputData("Education"),
                Experience = VacancyHelper.InputData("Experience"),
                Requirements = VacancyHelper.InputData("Requirements"),
                JobDescription = VacancyHelper.InputData("JobDescription"),
                Company = VacancyHelper.InputData("Company"),
                Contact = VacancyHelper.InputData("Contact"),
            };

            Console.WriteLine("Salary: ");

            newVacancy.Ad.Salary = new SalaryRange()
            {
                From = VacancyHelper.InputSalary("From:"),
                To = VacancyHelper.InputSalary("To:"),
            };
            return newVacancy;
        }
    }
}