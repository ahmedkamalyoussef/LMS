namespace LMS.Data.Entities
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question>? Questions { get; set; } = new HashSet<Question>();
        public ICollection<ExamResult>? ExamResults { get; set; } = new HashSet<ExamResult>();

    }
}
