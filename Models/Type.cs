namespace ASP_Final.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
