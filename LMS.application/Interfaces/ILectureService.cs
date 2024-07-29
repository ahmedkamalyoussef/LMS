using LMS.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Interfaces
{
    public interface ILectureService
    {
        public Task<LectureResultDTO> GetLecture(string id);
        public Task<List<LectureResultDTO>> GetCourseLectures(string courseId);
        public Task<bool> CreateLecture(LectureDTO book, IFormFile file);
        public Task<bool> UpdateLecture(string id, EditLectureDTO lectureId, IFormFile file);
        public Task<bool> DeleteLecture(string id);
        public Task<int> GetNumberOfLecturesInCourse(string courseId);
    }
}
