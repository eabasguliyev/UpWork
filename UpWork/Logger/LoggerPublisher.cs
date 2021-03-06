namespace UpWork.Logger
{
    public delegate void ErrorLogHandler(string message);

    public class LoggerPublisher
    {
        public static event ErrorLogHandler ErrorHandler;
        public static event ErrorLogHandler InfoHandler;

        public static void OnLogError(string message)
        {
            ErrorHandler.Invoke(message);
        }

        public static void OnLogInfo(string message)
        {
            InfoHandler.Invoke(message);
        }
    }
}