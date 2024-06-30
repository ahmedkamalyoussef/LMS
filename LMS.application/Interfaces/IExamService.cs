using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface IExamService
    {
        public Task<ExamResultDTO> GetExam(string id);
        public Task<double> GetExamResult(string examId);
        public Task<bool> SetExamResult(string examId,double result);
        public Task<List<ExamResultDTO>> GetCourseExams(string courseId);
        public Task<bool> CreateExam(ExamDTO examDTO);
        public Task<bool> UpdateExam(string id, ExamDTO examDTO);
        public Task<bool> DeleteExam(string id);
        public Task<int> GetNumberOfExamsInCourse(string courseId);
    }
}
