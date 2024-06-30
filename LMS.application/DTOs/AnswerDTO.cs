namespace LMS.Application.DTOs
{
    public class AnswerDTO
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public string QuestionId { get; set; }
    }
}
