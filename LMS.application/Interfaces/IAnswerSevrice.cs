using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface IAnswerSevrice
    {
        public Task<bool> CreateAnswer(AnswerDTO answerDto);
        public Task<bool> UpdateAnswer(string id, EditAnswerDTO answerDto);
        public Task<bool> DeleteAnswer(string id);
    }
}
