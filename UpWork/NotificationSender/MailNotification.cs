using UpWork.Abstracts;
using UpWork.Entities;
using UpWork.Network;

namespace UpWork.NotificationSender
{
    public class MailNotification
    {
        public static void Send(User user, Notification notification)
        {
            if (Mail.IsEnable)
            {
                Mail.SendMail(user.Mail, notification.Title, notification.Message);
            }
        }
    }
}