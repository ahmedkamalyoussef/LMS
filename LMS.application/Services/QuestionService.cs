using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class QuestionService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public async Task<Tuple<bool, string>> CreateQuestion(QuestionDTO questionDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var question = _mapper.Map<Question>(questionDto);
            await _unitOfWork.Questions.AddAsync(question);
            if (await _unitOfWork.SaveAsync() > 0)
                return Tuple.Create(true, question.Id);
            return Tuple.Create(false, "failed to create");
        }

        public async Task<bool> DeleteQuestion(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var question = await _unitOfWork.Questions.FindFirstAsync(c => c.Id == id) ?? throw new Exception("question not found");
            await _unitOfWork.Questions.RemoveAsync(question);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<List<QuestionResultDTO>> GetExamQuestions(string examId)
        {
            var questions = await _unitOfWork.Questions.FindAsync(b => b.ExamId == examId, null, null, includes: [q => q.Answers]);
            var questionsResult = _mapper.Map<IEnumerable<QuestionResultDTO>>(questions).ToList();
            return questionsResult;
        }

        public async Task<int> GetNumberOfQuestionsInExam(string examId)
        {
            var questions = await _unitOfWork.Questions.FindAsync(sc => sc.ExamId == examId);
            return questions.Count();
        }

        public async Task<QuestionResultDTO> GetQuestion(string id)
        {
            var question = await _unitOfWork.Questions.FindFirstAsync(c => c.Id == id, includes: [q => q.Answers]) ?? throw new Exception("question not found");
            var questionResult = _mapper.Map<QuestionResultDTO>(question);
            return questionResult;
        }

        public async Task<bool> UpdateQuestion(string id, EditQuestionDTO questionDTO)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var question = await _unitOfWork.Questions.FindFirstAsync(c => c.Id == id) ?? throw new Exception("question not found");
            _mapper.Map(questionDTO, question);
            await _unitOfWork.Questions.UpdateAsync(question);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
