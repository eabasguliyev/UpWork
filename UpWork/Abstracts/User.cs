using System.Collections.Generic;
using UpWork.Entities;

namespace UpWork.Abstracts
{
    public abstract class User:Id
    {
        private string _password;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }

        public string Password
        {
            get => _password;
            set
            {
                var hash = new Hash.Hash();
                _password = hash.GetHash(value);
            }
        }

        public string City { get; set; }
        public string Phone { get; set; }
        public int Age  { get; set; }

        public List<Notification> Notifications { get; set; }

        protected User()
        {
            Notifications = new List<Notification>();
        }
    }
}