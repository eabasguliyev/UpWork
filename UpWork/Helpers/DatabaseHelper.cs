using System.Linq;

namespace UpWork.Helpers
{
    public static class DatabaseHelper
    {
        public static bool CheckMail(this Database.Database database, string mail)
        {
            return database.Users.SingleOrDefault(u => u.Mail.Equals(mail)) == null;
        }

        public static bool CheckUsername(this Database.Database database, string username)
        {
            return database.Users.SingleOrDefault(u => u.Username.Equals(username)) == null;
        }
    }
}