using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Enums;
using UpWork.Helpers;
using UpWork.Logger;
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
            
            NotificationPublisher.EventHandler += MailNotification.Send;
            NotificationPublisher.EventHandler += ProgramNotification.Send;

            var consoleLogger = new ConsoleLogger();
            var fileLogger = new FileLogger();

            LoggerPublisher.ErrorHandler += consoleLogger.Error;
            LoggerPublisher.ErrorHandler += fileLogger.Error;

            LoggerPublisher.InfoHandler += consoleLogger.Info;
            LoggerPublisher.InfoHandler += fileLogger.Info;

            FileHelper.ReadFromJson(ref db);

            UserAccessSide.Start(db);
        }
    }
}