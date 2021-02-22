using System;

namespace UpWork.Exceptions
{
    public class RegisterException:ApplicationException
    {
        public RegisterException(string message):base(message)
        {
            
        }
    }
}