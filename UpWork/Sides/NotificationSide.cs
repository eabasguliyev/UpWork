using System;
using System.Linq;
using System.Windows.Forms;
using UpWork.Abstracts;
using UpWork.ConsoleInterface;
using UpWork.Helpers;

namespace UpWork.Sides
{
    public static class NotificationSide
    {
        public static void Start(User user)
        {
            while (true)
            {
                if (!ExceptionHandle.Handle(user.ShowShortNotfInfo))
                {
                    ConsoleScreen.Clear();
                    break;
                }

                var notificationId = UserHelper.InputGuid();


                var notification = user.Notifications.SingleOrDefault(n => n.Guid == notificationId);

                if (notification == null)
                {
                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        break;
                    continue;
                }

                Console.Clear();
                Console.WriteLine(notification);

                if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other notifications?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    break;
            }
        }
    }
}