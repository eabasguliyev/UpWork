using System;

namespace UpWork.Abstracts
{
    public abstract class Id
    {
        public Guid Guid { get; }

        protected Id()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"Id: {Guid}";
        }
    }
}