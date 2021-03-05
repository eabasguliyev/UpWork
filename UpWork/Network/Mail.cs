using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;

namespace UpWork.Network
{
    // turn on less secure apps access.
    /// <link>
    /// https://support.google.com/accounts/answer/6010255?hl=en
    /// </link>

    public static class Mail
    {
        public static bool IsEnable { get; set; }
        private static SmtpClient SmtpClient { get; set; }
        public static string SenderAddress { get; }
        private static string SenderPassword { get; }
        static Mail()
        {
            var file = @"Data\Config.json";

            if (File.Exists(file))
            {
                var json = string.Empty;

                using (var fs = File.OpenRead(file))
                    using (var sr = new StreamReader(fs, new UTF8Encoding(false))) 
                        json = sr.ReadToEnd();

                var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

                SenderAddress = configJson.Mail;
                SenderPassword = configJson.Password;

                IsEnable = true;

                SmtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(SenderAddress, SenderPassword),
                    EnableSsl = true
                };
            }
        }

        public static void SendMail(in string recipient, in string subject, in string body)
        {
            SmtpClient.Send(SenderAddress, recipient, subject, body);
        }
    }
}