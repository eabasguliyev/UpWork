using System;
using UpWork.Abstracts;
using UpWork.Entities;

namespace UpWork.NotificationSender
{
    public delegate void NotificationSender(User user, Notification notification);
    public class Publisher
    {
        public static event NotificationSender EventHandler;


        public static void OnSend(User user, Notification notification)
        {
            EventHandler?.Invoke(user, notification);
        }
    }
}