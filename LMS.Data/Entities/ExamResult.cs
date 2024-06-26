namespace LMS.Data.Entities
{
    public class ExamResult
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public double Result { get; set; }

    }
}
