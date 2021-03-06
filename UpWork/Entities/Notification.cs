using System;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Notification:Id
    {
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $@"Title: {Title}
Message:
{Message}";
        }

        public static Notification operator ++(Notification notification)
        {
            notification.IsRead = true;

            return notification;
        }
    }
}