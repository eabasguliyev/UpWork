namespace UpWork.Entities
{
    public class SalaryRange
    {
        public int From { get; set; }
        public int To { get; set; }

        public override string ToString()
        {
            return $@"From: {From}
To: {To}";
        }
    }
}