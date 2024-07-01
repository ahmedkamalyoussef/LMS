using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface ICourseService
    {
        public Task<CourseResultDTO> GetCourse(string id);
        public Task<List<CourseResultDTO>> GetAllCourses();
        public Task<bool> CreateCourse(CourseDTO course);
        public Task<bool> UpdateCourse(string id, CourseDTO course);
        public Task<bool> DeleteCourse(string id);
        public Task<int> GetNumberOfCourses();
        public Task<List<CourseResultDTO>> GetCoursesByTeacherId(string id);
        public Task<List<CourseResultDTO>> SearchForCources(string subject, string semester, double from, double to, int pageSize, int pageIndex);
        public Task<bool> EnrollingStudentInCourse(string StudentEmail ,string CourseCode);
        public Task<int> GetStudentCountInCourse(string courseId);

    }
}
