using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface IBookService
    {
        public Task<BookResultDTO> GetBook(string id);
        public Task<List<BookResultDTO>> GetCourseBooks(string courseId);
        public Task<bool> CreateBook(BookDTO book);
        public Task<bool> UpdateBook(string id, EditBookDTO book);
        public Task<bool> DeleteBook(string id);
        public Task<int> GetNumberOfBooksInCourse(string courseId);
    }
}
