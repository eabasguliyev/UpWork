using System;
using UpWork.Abstracts;

namespace UpWork.Entities
{
    public class Notification:Id
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $@"Title: {Title}
Message:
{Message}";
        }
    }
}