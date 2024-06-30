using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Entities;

namespace LMS.Application.Services
{
    public class AnswerService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IAnswerSevrice
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public async Task<bool> CreateAnswer(AnswerDTO answerDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var answer = _mapper.Map<Answer>(answerDto);
            await _unitOfWork.Answers.AddAsync(answer);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAnswer(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var answer = await _unitOfWork.Answers.FindFirstAsync(c => c.Id == id) ?? throw new Exception("Answer not found");
            await _unitOfWork.Answers.RemoveAsync(answer);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAnswer(string id, EditAnswerDTO answerDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var answer = await _unitOfWork.Answers.FindFirstAsync(c => c.Id == id) ?? throw new Exception("question not found");
            _mapper.Map(answerDto, answer);
            await _unitOfWork.Answers.UpdateAsync(answer);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
