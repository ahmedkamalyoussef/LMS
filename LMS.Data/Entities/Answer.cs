namespace LMS.Domain.Entities
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
    }
}
