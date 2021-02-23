using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Sides;

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


            UserAccessSide.Start(db);
        }
    }
}
