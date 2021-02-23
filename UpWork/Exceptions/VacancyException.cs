using System;

namespace UpWork.Exceptions
{
    public class VacancyException:ApplicationException
    {
        public VacancyException(string message):base(message)
        {
            
        }
    }
}