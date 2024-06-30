using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class EvaluationService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IEvaluationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public async Task<bool> AddEvaluationAsync(EvaluationDTO evaluationDTO)
        {
            var currentStudent = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var course = await _unitOfWork.Courses.FindFirstAsync(c => c.Id == evaluationDTO.CourseId) ?? throw new Exception("course not found");

            var oldEvaluation = await _unitOfWork.Evaluations.FindFirstAsync(e => e.StudentId == currentStudent.Id && e.CourseId == course.Id);
            if (oldEvaluation != null)
            {
                oldEvaluation.Value = evaluationDTO.Value;
                await _unitOfWork.Evaluations.UpdateAsync(oldEvaluation);
            }
            else
            {
                var newEvaluation = _mapper.Map<Evaluation>(evaluationDTO);
                newEvaluation.StudentId = currentStudent.Id;
                await _unitOfWork.Evaluations.AddAsync(newEvaluation);
            }
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteEvaluationAsync(string courseId)
        {
            var currentStudent = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var evaluation = await _unitOfWork.Evaluations.FindFirstAsync(e => e.CourseId == courseId && e.StudentId == currentStudent.Id) ?? throw new Exception("question not found");
            await _unitOfWork.Evaluations.RemoveAsync(evaluation);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
