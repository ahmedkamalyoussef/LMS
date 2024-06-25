namespace LMS.Data.Entities
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double Result { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
