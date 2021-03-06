using System.IO;
using System.Text;
using UpWork.Interfaces;

namespace UpWork.Logger
{
    public class FileLogger : ILogger
    {
        public static string ErrorFile { get; set; } = "error.log";
        public static string InfoFile { get; set; } = "info.log";
        public void Error(string message)
        {
            var sb = new StringBuilder();

            sb.Append($"Error: {message}\n");
            sb.Append("------------------------------------------------------\n");

            WriteData(ErrorFile, sb.ToString());
        }

        public void Info(string message)
        {
            var sb = new StringBuilder();

            sb.Append($"Info: {message}\n");
            sb.Append("------------------------------------------------------\n");

            WriteData(InfoFile, sb.ToString());
        }

        private void WriteData(string fileName, string data)
        {
            using (var fs = new FileStream(fileName, FileMode.Append))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF32))
                {
                    sw.WriteLine(data);
                }
            }
        }
    }
}