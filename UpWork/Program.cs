using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.NotificationSender;
using UpWork.Sides;
using UpWork.Sides.UserAccess;

namespace UpWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database.Database();
            
            Publisher.EventHandler += MailNotification.Send;
            Publisher.EventHandler += ProgramNotification.Send;

            //db.Users.Add(new Worker()
            //{
            //    Name = "Elgun",
            //    Surname = "Abasquliyev",
            //    Age = 21,
            //    City = "Baku",
            //    Username = "elgun",
            //    Password = "elgun",
            //    Mail = "elgun@gmail.com",
            //    Phone = "077433434"
            //});

            //(db.Users[0] as Worker).Cvs.Add(new Cv()
            //{
            //    Category = Data.Data.Categories[0],
            //    Education = Data.Data.Educations[2],
            //    Experience = Data.Data.Experiences[1],
            //    Salary = Data.Data.Salaries[2],
            //    HonorsDiploma = false,
            //    Languages = new List<Language>() { new Language() { Name = "English", Level = SkillLevelEnum.Intermediate } },
            //    Skills = new List<Skill>() { new Skill() { Name = "c++", Level = SkillLevelEnum.Intermediate } },
            //});

            //db.Users.Add(new Employer()
            //{
            //    Name = "Abil",
            //    Surname = "Yaqublu",
            //    Username = "abil",
            //    Password = "abil",
            //    Mail = "abil@gmail.com",
            //    City = "Sumqayit",
            //    Age = 16,
            //    Phone = "1234"
            //});

            //(db.Users[1] as Employer).Vacancies.Add(new Vacancy()
            //{
            //    Contact = new Contact() { Mail = "abil@gmail.com" },
            //    Ad = new Advert()
            //    {
            //        Category = Data.Data.Categories[3],
            //        Company = "Code",
            //        JobDescription = "hershey",
            //        Contact = "Abil",
            //        Education = Data.Data.Educations[2],
            //        Experience = Data.Data.Experiences[3],
            //        Position = "Manager",
            //        Region = Data.Data.Regions[2],
            //        Requirements = "allah verenden",
            //        Salary = Data.Data.Salaries[3]
            //    }
            //});

            //(db.Users[1] as Employer).Vacancies.Add(new Vacancy()
            //{
            //    Contact = new Contact() { Mail = "abil@gmail.com" },
            //    Ad = new Advert()
            //    {
            //        Category = "Programming",
            //        Company = "Step",
            //        JobDescription = "lab lab",
            //        Contact = "Resul",
            //        Education = "Uni",
            //        Experience = "3 il",
            //        Position = "C# Developer",
            //        Region = "Baku",
            //        Requirements = "allah verenden",
            //        Salary = Data.Data.Salaries[4],
            //    }
            //});

            Data.Data.ReadFromJson(ref db);
            //Data.Data.WriteToJson(db);

            UserAccessSide.Start(db);

        }
    }
}