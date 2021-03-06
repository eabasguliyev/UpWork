using System;
using System.Collections.Generic;
using UpWork.ConsoleInterface;
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
                

                LoggerPublisher.OnLogError(e.Message);
            }

            return false;
        }

        public static IList<T2> Handle<T1, T2>(Func<T1, IList<T2>, IList<T2>> func, T1 data, IList<T2> data2)
        {
            try
            {
                return func.Invoke(data, data2);
            }
            catch (Exception e)
            {
                LoggerPublisher.OnLogError(e.Message);
                ConsoleScreen.Clear();
                return null;
            }
        }
        public static bool Handle(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception e)
            {
                

                LoggerPublisher.OnLogError(e.Message);
            }

            return false;
        }
    }
}