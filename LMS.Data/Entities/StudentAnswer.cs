namespace LMS.Data.Entities
{
    public class StudentAnswer
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string AnswerId { get; set; }
        public Answer Answer { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
