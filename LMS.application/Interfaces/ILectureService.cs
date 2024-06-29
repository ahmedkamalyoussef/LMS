using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface ILectureService
    {
        public Task<LectureResultDTO> GetLecture(string id);
        public Task<List<LectureResultDTO>> GetCourseLectures(string courseId);
        public Task<bool> CreateLecture(LectureDTO book);
        public Task<bool> UpdateLecture(string id, EditLectureDTO lectureId);
        public Task<bool> DeleteLecture(string id);
        public Task<int> GetNumberOfLecturesInCourse(string courseId);
    }
}
