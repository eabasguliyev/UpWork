using System;
using UpWork.Logger;

namespace UpWork
{
    public static class ExceptionHandle
    {
        public static bool Handle<T>(Action<T> action, T data)
        {
            try
            {
                action.Invoke(data);
                return true;
            }
            catch (Exception e)
            {
                var logger = new ConsoleLogger();

                logger.Error(e.Message);
            }

            return false;
        }
    }
}