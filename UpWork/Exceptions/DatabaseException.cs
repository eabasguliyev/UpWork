using System;
using System.Runtime.Hosting;

namespace UpWork.Exceptions
{
    public class DatabaseException:ApplicationException
    {
        public DatabaseException(string message):base(message)
        {
            
        }
    }
}