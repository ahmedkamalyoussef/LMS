namespace LMS.Data.Entities
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
