using LMS.Domain.Entities;

namespace LMS.Application.DTOs
{
    public class QuestionResultDTO
    {
        public string Id { get; set; } 
        public string Content { get; set; }
        public ICollection<AnswerResultDTO>? Answers { get; set; } 
    }
}
