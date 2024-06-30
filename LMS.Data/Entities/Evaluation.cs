namespace LMS.Data.Entities
{
    public class Evaluation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Value { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
