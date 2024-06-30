using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface IQuestionService
    {
        public Task<QuestionResultDTO> GetQuestion(string id);
        public Task<List<QuestionResultDTO>> GetExamQuestions(string examId);
        public Task<bool> CreateQuestion(QuestionDTO questionDto);
        public Task<bool> UpdateQuestion(string id, EditQuestionDTO questionDTO);
        public Task<bool> DeleteQuestion(string id);
        public Task<int> GetNumberOfQuestionsInExam(string examId);
    }
}
