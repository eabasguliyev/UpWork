using System.Collections.Generic;

namespace UpWork.Entities
{
    public class Contact
    {
        public string Mail { get; set; }
        public IList<string> Phones { get; set; }

        public Contact()
        {
            Phones = new List<string>();
        }
    }
}