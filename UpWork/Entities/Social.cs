namespace UpWork.Entities
{
    public class Social
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            return $@"Name: {Name}
Link: {Link}";
        }
    }
}