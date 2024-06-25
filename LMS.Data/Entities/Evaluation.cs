namespace LMS.Data.Entities
{
    public class Evaluation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Value { get; set; }
    }
}
