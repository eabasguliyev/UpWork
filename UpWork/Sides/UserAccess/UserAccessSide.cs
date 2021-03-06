using System;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Exceptions;

using UpWork.Helpers;
using UpWork.Logger;
using UpWork.Sides.Employee;
using UpWork.Sides.Employer;
using UpWork.UserAccess;

namespace UpWork.Sides.UserAccess
{
    public static class UserAccessSide
    {
        public static void Start(Database.Database db)
        {
            

            var userAccessLoop = true;

            while (userAccessLoop)
            {
                Console.Title = "UpWork";
                ConsoleScreen.PrintMenu(ConsoleScreen.UserAccess, ConsoleColor.DarkGreen);

                var userAccess = (UserAccessEnum)ConsoleScreen.Input(ConsoleScreen.UserAccess.Count);

                Console.Clear();

                switch (userAccess)
                {
                    case UserAccessEnum.Login:
                        {
                            LoginSide(db);
                            break;
                        }
                    case UserAccessEnum.Register:
                        {
                            RegisterSide(db);
                            break;
                        }
                    case UserAccessEnum.Exit:
                    {
                        userAccessLoop = false;
                        break;
                    }
                }
            }
        }

        private static void LoginSide(Database.Database db)
        {
            

            var loginLoop = true;
            while (loginLoop)
            {
                Console.Title = "Login";
                Console.WriteLine("Username: ");

                var username = UserHelper.GetString("Username can not be empty!");

                Console.WriteLine("Password: ");

                var password = UserHelper.GetString("Password can not be empty!");

                Console.Clear();
                
                var credentials = new Credentials() { Username = username, Password = password };

                try
                {
                    var loggedUser = UpWork.UserAccess.UserAccess.Login(credentials, db.Users);

                    if (loggedUser is Worker worker)
                    {
                        WorkerSide.Start(worker, db);
                    }
                    else if (loggedUser is Entities.Employer employer)
                    {
                        EmployerSide.Start(employer, db);
                    }
                    break;
                }
                catch (Exception e) when (e is DatabaseException)
                {
                    LoggerPublisher.OnLogError($"There is no user associated this username -> {username}");
                }
                catch (Exception e)
                {
                    LoggerPublisher.OnLogError(e.Message);
                }

                if (ConsoleScreen.DisplayMessageBox("Error", "Do yo want try again?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                    == DialogResult.No)
                {
                    loginLoop = false;
                }
                Console.Clear();
            }

        }

        private static void RegisterSide(Database.Database db)
        {
            

            while (true)
            {
                Console.Title = "Register";
                GetUserData(db, out string name, out string surname, out string mail, out string phone, out string username, out string password, out int age, out string city);

                User newUser = null;

                Console.Clear();

                Console.WriteLine("Who are you?");

                ConsoleScreen.PrintMenu(ConsoleScreen.UserType, ConsoleColor.Blue);

                var userType = (UserTypeEnum)(ConsoleScreen.Input(ConsoleScreen.UserType.Count));

                Console.Clear();

                if (userType == UserTypeEnum.Worker)
                    newUser = new Worker()
                    {
                        Name = name,
                        Surname = surname,
                        Mail = mail,
                        Phone = phone,
                        Username = username,
                        Password = password,
                        City = city,
                        Age = age,
                    };
                else
                    newUser = new Entities.Employer()
                    {
                        Name = name,
                        Surname = surname,
                        Mail = mail,
                        Phone = phone,
                        Username = username,
                        Password = password,
                        City = city,
                        Age = age,
                    };

                try
                {
                    UpWork.UserAccess.UserAccess.SendConfirmationCode(newUser.Mail);

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Confirmation code: ");

                        if (UpWork.UserAccess.UserAccess.ConfirmationCode ==
                            UserHelper.GetNumeric(NumericTypes.INT).ToString())
                            break;

                        LoggerPublisher.OnLogError("Confirmation code is wrong!");
                    }

                    UpWork.UserAccess.UserAccess.Register(newUser, db.Users);
                    FileHelper.WriteToJson(db);
                    LoggerPublisher.OnLogInfo("Successfully created account. You can login now.");
                    ConsoleScreen.Clear();
                    break;
                }
                catch (Exception e)
                {
                    LoggerPublisher.OnLogError(e.Message);
                    ConsoleScreen.Clear();
                }
            }
        }
        private static void GetUserData(Database.Database db, out string name, out string surname, out string mail, out string phone, out string username, out string password, out int age, out string city)
        {
            
            Console.WriteLine("Name: ");

            name = UserHelper.GetString("Name can not be empty!");

            Console.WriteLine("Surname: ");

            surname = UserHelper.GetString("Surname can not be empty");

            Console.WriteLine("Mail: ");

            mail = string.Empty;

            while (true)
            {
                mail = UserHelper.GetString("Mail can not be empty");

                if (ExceptionHandle.Handle(UserHelper.ValidateMail, mail))
                {
                    if (db.CheckMail(mail))
                        break;

                    LoggerPublisher.OnLogError($"User exists associated this mail -> {mail}!");
                }
            }

            Console.WriteLine("Phone: ");

            phone = string.Empty;

            while (true)
            {
                phone = UserHelper.GetString("Phone can not be empty!");

                if (ExceptionHandle.Handle(UserHelper.ValidatePhone, phone))
                    break;
            }


            Console.WriteLine("Username: ");

            username = string.Empty;

            while (true)
            {
                username = UserHelper.GetString("Username can not be empty");

                if (db.CheckUsername(username))
                    break;

                LoggerPublisher.OnLogError($"User exists associated this username -> {username} ");
            }

            Console.WriteLine("Password: ");

            var hash = new Hash.Hash();

            password = hash.GetHash(UserHelper.GetString("Password can not be empty!"));

            Console.WriteLine("City: ");


            city = UserHelper.GetString("City can not be empty");


            Console.WriteLine("Age: ");


            age = UserHelper.GetNumeric(NumericTypes.INT);
        }
    }
}