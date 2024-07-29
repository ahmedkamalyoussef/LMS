using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Consts;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Services
{
    public class BookService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers, CloudinaryService cloudinaryService) : IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;
        private readonly CloudinaryService _cloudinaryService = cloudinaryService;

        public async Task<bool> CreateBook(BookDTO bookDto, IFormFile file)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = _mapper.Map<Book>(bookDto);
            if (file != null)
                book.BookUrl = await _userHelpers.AddFileAsync(file, Folder.Book);
            //book.BookUrl = await _cloudinaryService.UploadFileAsync(bookDto.Book);
            await _unitOfWork.Books.AddAsync(book);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteBook(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("book not found");
            var oldImgPath = book.BookUrl;
            await _unitOfWork.Books.RemoveAsync(book);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Book);
                //await _cloudinaryService.DeleteRawFileAsync(oldImgPath);
                return true;
            }
            return false;
        }

        public async Task<LectureDtoResultDTO> GetBook(string id)
        {
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("book not found");
            var bookResult = _mapper.Map<LectureDtoResultDTO>(book);
            return bookResult;
        }

        public async Task<List<LectureDtoResultDTO>> GetCourseBooks(string courseId)
        {
            var books = await _unitOfWork.Books.FindAsync(b => b.CourseId == courseId);
            var coursesResult = _mapper.Map<IEnumerable<LectureDtoResultDTO>>(books).ToList();
            return coursesResult;
        }

        public async Task<int> GetNumberOfBooksInCourse(string courseId)
        {
            var books = await _unitOfWork.Books.FindAsync(sc => sc.CourseId == courseId);
            return books.Count();
        }

        public async Task<bool> UpdateBook(string id, EditBookDTO bookDto, IFormFile file)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var book = await _unitOfWork.Books.FindFirstAsync(c => c.Id == id) ?? throw new Exception("course not found");
            _mapper.Map(bookDto, book);
            var oldImgPath = book.BookUrl;
            if (file != null)
                book.BookUrl = await _userHelpers.AddFileAsync(file, Folder.Book);
            //book.BookUrl = await _cloudinaryService.UploadFileAsync(bookDto.Book);
            await _unitOfWork.Books.UpdateAsync(book);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Book);
                //await _cloudinaryService.DeleteRawFileAsync(oldImgPath);
                return true;
            }
            return false;
        }
    }
}
