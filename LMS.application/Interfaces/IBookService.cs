using LMS.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Interfaces
{
    public interface IBookService
    {
        public Task<LectureDtoResultDTO> GetBook(string id);
        public Task<List<LectureDtoResultDTO>> GetCourseBooks(string courseId);
        public Task<bool> CreateBook(BookDTO book, IFormFile file);
        public Task<bool> UpdateBook(string id, EditBookDTO book, IFormFile? file);
        public Task<bool> DeleteBook(string id);
        public Task<int> GetNumberOfBooksInCourse(string courseId);
    }
}
