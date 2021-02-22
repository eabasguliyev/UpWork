using System;
using System.Runtime.Hosting;

namespace UpWork.Exceptions
{
    public class LoginException:ApplicationException
    {
        public LoginException(string message):base(message)
        {
            
        }
    }
}