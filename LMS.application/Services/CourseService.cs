using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Consts;

namespace LMS.Application.Services
{
    public class CourseService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers, CloudinaryService cloudinaryService) : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;
        private readonly CloudinaryService _cloudinaryService = cloudinaryService;
        public async Task<bool> CreateCourse(CourseDTO courseDto)
        {
            var teacher = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var course = _mapper.Map<Course>(courseDto);
            course.TeacherId = teacher.Id;
            if (courseDto.CourseImage != null)
                course.Image = await _userHelpers.AddFileAsync(courseDto.CourseImage, Folder.Image);
            //course.Image = await _cloudinaryService.UploadImageAsync(courseDto.CourseImage);
            await _unitOfWork.Courses.AddAsync(course);
            return await _unitOfWork.SaveAsync() > 0;
        }
        public async Task<bool> UpdateCourse(string id, CourseDTO courseDTO)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var course = await _unitOfWork.Courses.FindFirstAsync(c => c.Id == id) ?? throw new Exception("course not found");
            var oldImgPath = course.Image;
            _mapper.Map(courseDTO, course);
            if (courseDTO.CourseImage != null)
            {
                course.Image = await _userHelpers.AddFileAsync(courseDTO.CourseImage, Folder.Image);
                //course.Image = await _cloudinaryService.UploadImageAsync(courseDTO.CourseImage);
            }
            await _unitOfWork.Courses.UpdateAsync(course);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Image);
                //await _cloudinaryService.DeleteImageAsync(oldImgPath);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCourse(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var course = await _unitOfWork.Courses.FindFirstAsync(c => c.Id == id, includes: [c => c.Books, c => c.Lectures]) ?? throw new Exception("course not found");
            var oldImgPath = course.Image;

            await _unitOfWork.Courses.RemoveAsync(course);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Image);
                //await _cloudinaryService.DeleteImageAsync(oldImgPath);
                foreach (Book book in course.Books)
                    await _userHelpers.DeleteFileAsync(book.BookUrl, Folder.Book);
                foreach (Lecture lecture in course.Lectures)
                    await _userHelpers.DeleteFileAsync(lecture.LectureUrl, Folder.Lecture);
                return true;
            }
            return false;
        }

        public async Task<bool> EnrollingStudentInCourse(string StudentEmail, string courseCode)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var student = await _unitOfWork.Students.FindFirstAsync(s => s.Email == StudentEmail) ?? throw new Exception("student not found");
            var course = await _unitOfWork.Courses.FindFirstAsync(c => c.Code == courseCode) ?? throw new Exception("course not found");
            if (currentUser.Id != course.TeacherId)
                throw new Exception("course not found");
            var newStudentCourse = new StudentCourse { CourseId = course.Id, StudentId = student.Id };
            await _unitOfWork.StudentCourses.AddAsync(newStudentCourse);
            return await _unitOfWork.SaveAsync() > 0;

        }

        public async Task<List<CourseResultDTO>> GetAllCourses()
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var courses = await _unitOfWork.Courses.GetAllAsync(orderBy: course => course.Name,
            direction: OrderDirection.Ascending,
            includes:
            [
                course => course.Teacher,
            ]);
            var coursesResult = _mapper.Map<IEnumerable<CourseResultDTO>>(courses).ToList();
            foreach (var course in coursesResult)
            {
                var evaluations = await _unitOfWork.Evaluations.FindAsync(e => e.CourseId == course.Id);
                course.Evaluation = CalculateAverageRate(evaluations.ToList());
                var studentCuorse = await _unitOfWork.StudentCourses.FindFirstAsync(sc => sc.CourseId == course.Id && sc.StudentId == currentUser.Id);
                if (studentCuorse != null) course.IsEnrolled = true;
                else course.IsEnrolled = false;
            }
            return coursesResult;
        }

        public async Task<CourseResultDTO> GetCourse(string id)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var course = await _unitOfWork.Courses.FindFirstAsync(c => c.Id == id,
            includes:
            [
                course => course.Teacher,
                cource => cource.Evaluations

            ]);
            var coursesResult = _mapper.Map<CourseResultDTO>(course);
            coursesResult.Evaluation = CalculateAverageRate(course.Evaluations.ToList());
            var studentCuorse = await _unitOfWork.StudentCourses.FindFirstAsync(sc => sc.CourseId == course.Id && sc.StudentId == currentUser.Id);
            if (studentCuorse != null) coursesResult.IsEnrolled = true;
            else coursesResult.IsEnrolled = false;
            return coursesResult;
        }

        public async Task<List<CourseResultDTO>> GetCoursesByTeacherId(string id)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var courses = await _unitOfWork.Courses.FindAsync(c => c.TeacherId == id,
            orderBy: course => course.Name,
            direction: OrderDirection.Ascending,
            includes:
            [
                c => c.Teacher,
            ]);
            var coursesResult = _mapper.Map<IEnumerable<CourseResultDTO>>(courses).ToList();
            foreach (var course in coursesResult)
            {
                var evaluations = await _unitOfWork.Evaluations.FindAsync(e => e.CourseId == course.Id);
                course.Evaluation = CalculateAverageRate(evaluations.ToList());
                var studentCuorse = await _unitOfWork.StudentCourses.FindFirstAsync(sc => sc.CourseId == course.Id && sc.StudentId == currentUser.Id);
                if (studentCuorse != null) course.IsEnrolled = true;
                else course.IsEnrolled = false;
            }
            return coursesResult;
        }

        public async Task<int> GetNumberOfCourses()
        {
            return await _unitOfWork.Courses.CountAsync();
        }

        public async Task<int> GetStudentCountInCourse(string courseId)
        {
            var studentCourses = await _unitOfWork.StudentCourses.FindAsync(sc => sc.CourseId == courseId);
            return studentCourses.Count();
        }

        public async Task<List<CourseResultDTO>> SearchForCources(string subject, string semester, double from, double to, int pageSize, int pageIndex)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var courses = await _unitOfWork.Courses.FilterAsync(pageSize, pageIndex, [c => c.MaterialName.Contains(subject) || c.Name.Contains(subject)
            || subject.Contains(c.MaterialName) || subject.Contains(c.Name),
                c => semester.Contains(c.Semester) || c.Semester.Contains(semester),
                c => c.Price >= from && c.Price <= to
            ],
            orderBy: course => course.Name,
            direction: OrderDirection.Descending,
            includes:
            [
                c => c.Teacher
            ]);
            var coursesResult = _mapper.Map<IEnumerable<CourseResultDTO>>(courses).ToList();
            foreach (var course in coursesResult)
            {
                var evaluations = await _unitOfWork.Evaluations.FindAsync(e => e.CourseId == course.Id);
                course.Evaluation = CalculateAverageRate(evaluations.ToList());
                var studentCuorse = await _unitOfWork.StudentCourses.FindFirstAsync(sc => sc.CourseId == course.Id && sc.StudentId == currentUser.Id);
                if (studentCuorse != null) course.IsEnrolled = true;
                else course.IsEnrolled = false;
            }
            return coursesResult;
        }



        #region private methods
        private double CalculateAverageRate(List<Evaluation>? evaluations)
        {
            if (evaluations == null || !evaluations.Any())
            {
                return 0;
            }
            double totalRate = 0;
            foreach (var evaluation in evaluations)
            {
                totalRate += evaluation.Value;
            }
            return totalRate / evaluations.Count();
        }
        #endregion
    }
}
