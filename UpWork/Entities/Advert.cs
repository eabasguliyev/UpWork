namespace UpWork.Entities
{
    public class Advert
    {
        public string Category { get; set; }
        public string Position { get; set; }
        public string Region { get; set; }
        public SalaryRange Salary { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }

        public string Requirements { get; set; }
        public string JobDescription { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
    }
}