using System;

namespace UpWork.Exceptions
{
    public class NotFoundCvException:ApplicationException
    {
        public NotFoundCvException(string message):base(message)
        {
            
        }
    }
}