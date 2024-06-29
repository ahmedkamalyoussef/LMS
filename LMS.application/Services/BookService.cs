using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class BookService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public Task<bool> CreateBook(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBook(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BookResultDTO> GetBook(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookResultDTO>> GetCourseBooks(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfBooksInCourse()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBook(string id, BookDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
