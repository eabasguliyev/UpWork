using System;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
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
            UserAccessSide.Start(db);
        }
    }
}
