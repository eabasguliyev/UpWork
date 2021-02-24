using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Sides;
using UpWork.Sides.UserAccess;

namespace UpWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database.Database();

            db.Users.Add(new Worker()
            {
                Name = "Elgun",
                Surname = "Abasquliyev",
                Age = 21,
                City = "Baku",
                Username = "elgun",
                Password = "elgun",
                Mail = "elgun@gmail.com",
                Phone = "077433434"
            });

            (db.Users[0] as Worker).Cvs.Add(new Cv()
            {
                Speciality = "Programmer",
                School = "233",
                HonorsDiploma = false,
                Languages = new List<Language>() { new Language() { Name = "English", Level = SkillLevelEnum.Intermediate} },
                Skills = new List<Skill>() { new Skill() { Name = "c++", Level = SkillLevelEnum.Intermediate} },
                UniScore = 450
            });

            db.Users.Add(new Employer()
            {
                Name = "Abil",
                Surname = "Yaqublu",
                Username = "abil",
                Password = "abil",
                Mail = "abil@gmail.com",
                City = "Sumqayit",
                Age = 16,
                Phone = "1234"
            });

            (db.Users[1] as Employer).Vacancies.Add(new Vacancy()
            {
                Contact = new Contact(){Mail = "abil@gmail.com"},
                Ad = new Advert()
                {
                    Category = "IT",
                    Company = "Code",
                    JobDescription = "hershey",
                    Contact = "Abil",
                    Education = "Highschool",
                    Experience = "2 il",
                    Position = "Manager",
                    Region = "Baku",
                    Requirements = "allah verenden",
                    Salary = new SalaryRange() { From = 200, To = 300},
                }
            });

            (db.Users[1] as Employer).Vacancies.Add(new Vacancy()
            {
                Contact = new Contact() { Mail = "abil@gmail.com" },
                Ad = new Advert()
                {
                    Category = "Programming",
                    Company = "Step",
                    JobDescription = "lab lab",
                    Contact = "Resul",
                    Education = "Uni",
                    Experience = "3 il",
                    Position = "C# Developer",
                    Region = "Baku",
                    Requirements = "allah verenden",
                    Salary = new SalaryRange() { From = 400, To = 600 },
                }
            });

            UserAccessSide.Start(db);
        }
    }
}
