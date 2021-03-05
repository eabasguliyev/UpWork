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
            

            Data.Data.ReadFromJson(ref db);

            UserAccessSide.Start(db);

        }
    }
}