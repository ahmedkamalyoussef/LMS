using LMS.Application.DTOs;
using LMS.Application.Interfaces;

namespace LMS.Application.Services
{
    public class LectureService : ILectureService
    {
        public Task<bool> CreateLecture(LectureDTO book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLecture(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookResultDTO>> GetCourseLectures(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<BookResultDTO> GetLecture(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfLecturesInCourse(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLecture(string id, EditLectureDTO lectureId)
        {
            throw new NotImplementedException();
        }
    }
}
