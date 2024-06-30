using LMS.Data.Entities;

namespace LMS.Domain.Entities
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public bool IsCorrect { get; set; } 
        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
