using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class ExamService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IExamService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public async Task<bool> CreateExam(ExamDTO examDTO)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var exam = _mapper.Map<Exam>(examDTO);
            await _unitOfWork.Exams.AddAsync(exam);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteExam(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var exam = await _unitOfWork.Exams.FindFirstAsync(c => c.Id == id) ?? throw new Exception("exam not found");
            await _unitOfWork.Exams.RemoveAsync(exam);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<List<ExamResultDTO>> GetCourseExams(string courseId)
        {
            var exams = await _unitOfWork.Exams.FindAsync(b => b.CourseId == courseId);
            var examsResult = _mapper.Map<IEnumerable<ExamResultDTO>>(exams).ToList();
            return examsResult;
        }

        public async Task<ExamResultDTO> GetExam(string id)
        {
            var exam = await _unitOfWork.Exams.FindFirstAsync(c => c.Id == id) ?? throw new Exception("exam not found");
            var examResult = _mapper.Map<ExamResultDTO>(exam);
            return examResult;
        }

        public async Task<double> GetExamResult(string examId)
        {
            var currentStudent = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var exam = await _unitOfWork.Exams.FindFirstAsync(c => c.Id == examId) ?? throw new Exception("exam not found");
            var examResult = await _unitOfWork.ExamResults.FindFirstAsync(c => c.ExamId == examId&&c.StudentId==currentStudent.Id) ?? throw new Exception("exam result not found");
            return examResult.Result;
        }

        public async Task<int> GetNumberOfExamsInCourse(string courseId)
        {
            var exams = await _unitOfWork.Exams.FindAsync(sc => sc.CourseId == courseId);
            return exams.Count();
        }

        public async Task<bool> SetExamResult(string examId, double result)
        {
            var currentStudent = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var exam = await _unitOfWork.Exams.FindFirstAsync(c => c.Id == examId) ?? throw new Exception("exam not found");
            ExamResult examResult = new() { ExamId = examId, StudentId = currentStudent.Id, Result = result };
            await _unitOfWork.ExamResults.AddAsync(examResult);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateExam(string id, ExamDTO examDTO)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var exam = await _unitOfWork.Exams.FindFirstAsync(c => c.Id == id) ?? throw new Exception("exam not found");
            _mapper.Map(examDTO, exam);
            await _unitOfWork.Exams.UpdateAsync(exam);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
