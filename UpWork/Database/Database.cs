using System.Collections.Generic;
using UpWork.Abstracts;

namespace UpWork.Database
{
    public class Database
    {
        public IList<User> Users { get; set; }

        public Database()
        {
            Users = new List<User>();
        }
    }
}