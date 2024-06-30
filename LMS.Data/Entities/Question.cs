using LMS.Domain.Entities;

namespace LMS.Data.Entities
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public ICollection<Answer>? Answers { get; set; } = new HashSet<Answer>();

    }
}
