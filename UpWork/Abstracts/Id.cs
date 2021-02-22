using System;

namespace UpWork.Abstracts
{
    public abstract class Id
    {
        public Guid Guid { get; set; }

        protected Id()
        {
            Guid = Guid.NewGuid();
        }
    }
}