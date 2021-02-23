using System.Collections.Generic;
using System.Text;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Contact:Id
    {
        public string Mail { get; set; }
        public IList<string> Phones { get; set; }


        public override string ToString()
        {
            return $@"{base.ToString()}
Mail: {Mail}
Phones: 
{GetPhones()}";
        }

        public Contact()
        {
            Phones = new List<string>();
        }


        private string GetPhones()
        {
            if (Phones.Count == 0)
                return "";

            var sb = new StringBuilder();

            for (var i = 0; i < Phones.Count; i++)
            {
                sb.Append($"{i + 1}. {Phones[i]}\n");
            }

            return sb.ToString();
        }
    }
}