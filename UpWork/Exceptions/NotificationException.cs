using System;

namespace UpWork.Exceptions
{
    public class NotificationException:ApplicationException
    {
        public NotificationException(string message):base(message)
        {
            
        }
    }
}