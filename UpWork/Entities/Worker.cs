using System.Collections.Generic;
using UpWork.Abstracts;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Worker:User
    {
        public IList<ICv> Cvs { get; set; }

        public Worker()
        {
            Cvs = new List<ICv>();
        }
    }
}