using System;

namespace UpWork.Entities
{
    public class Timeline
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return $@"Start: {Start.ToShortDateString()}
End: {End.ToShortDateString()}";
        }
    }
}