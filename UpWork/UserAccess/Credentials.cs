namespace UpWork.UserAccess
{
    public struct Credentials
    {
        private string _password;
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
    }
}