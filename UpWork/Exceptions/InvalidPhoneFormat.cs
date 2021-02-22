using System;

namespace UpWork.Exceptions
{
    public class InvalidPhoneFormat:ApplicationException
    {
        public InvalidPhoneFormat(string message):base(message)
        {
            
        }
    }
}