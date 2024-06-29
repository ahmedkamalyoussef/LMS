using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface ICourseService
    {
        public Task<CourseResultDTO> GetCourse(string id);
        public Task<List<CourseResultDTO>> GetAllCourses();
        public Task<List<CourseResultDTO>> GetCoursesByTeacherId(string id);
        public Task<List<CourseResultDTO>> SearchForCources(string crateria);
        public Task<bool> CreateCourse(CourseDTO course);
        public Task<bool> UpdateCourse(string id, CourseDTO course);
        public Task<bool> DeleteCourse(string id);
        public Task<bool> EnrollingStudentInCourse(string StudentEmail ,string CourseCode);
        public Task<int> GetStudentCountInCourse(string courseId);
        public Task<int> GetNumberOfCourses();

    }
}
