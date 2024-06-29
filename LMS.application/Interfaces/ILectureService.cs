using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface ILectureService
    {
        public Task<BookResultDTO> GetLecture(string id);
        public Task<List<BookResultDTO>> GetCourseLectures(string courseId);
        public Task<bool> CreateLecture(LectureDTO book);
        public Task<bool> UpdateLecture(string id, EditLectureDTO lectureId);
        public Task<bool> DeleteLecture(string id);
        public Task<int> GetNumberOfLecturesInCourse(string courseId);
    }
}
