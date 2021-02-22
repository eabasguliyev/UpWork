using System;

namespace UpWork.Exceptions
{
    public class MailException:ApplicationException
    {
        public MailException(string message):base(message)
        {
            
        }
    }
}