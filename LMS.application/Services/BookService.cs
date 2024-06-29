using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class BookService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public async Task<bool> CreateBook(BookDTO bookDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteBook(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("book not found");
            await _unitOfWork.Books.RemoveAsync(book);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<BookResultDTO> GetBook(string id)
        {
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("book not found");
            var bookResult = _mapper.Map<BookResultDTO>(book);
            return bookResult;
        }

        public async Task<List<BookResultDTO>> GetCourseBooks(string courseId)
        {
            var books = await _unitOfWork.Books.FindAsync(b => b.CourseId == courseId);
            var coursesResult = _mapper.Map<IEnumerable<BookResultDTO>>(books).ToList();
            return coursesResult;
        }

        public async Task<int> GetNumberOfBooksInCourse(string courseId)
        {
            var books = await _unitOfWork.Books.FindAsync(sc => sc.CourseId == courseId);
            return books.Count();
        }

        public async Task<bool> UpdateBook(string id, EditBookDTO bookDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("course not found");
            _mapper.Map(bookDto, book);
            await _unitOfWork.Books.UpdateAsync(book);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
