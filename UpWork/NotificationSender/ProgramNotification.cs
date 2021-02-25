using UpWork.Abstracts;
using UpWork.Entities;

namespace UpWork.NotificationSender
{
    class ProgramNotification
    {
        public static void Send(User user, Notification notf)
        {
            user.Notifications.Add(notf);
        }
    }
}
