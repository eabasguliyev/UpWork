using System;

namespace UpWork.Exceptions
{
    public class InvalidLinkFormat:ApplicationException
    {
        public InvalidLinkFormat(string message):base(message)
        {
            
        }
    }
}