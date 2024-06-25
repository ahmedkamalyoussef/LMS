namespace LMS.Data.Entities
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string BookUrl { get; set; }
    }
}
