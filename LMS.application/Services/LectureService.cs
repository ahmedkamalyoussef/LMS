using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Consts;

namespace LMS.Application.Services
{
    public class LectureService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers, CloudinaryService cloudinaryService) : ILectureService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;
        private readonly CloudinaryService _cloudinaryService = cloudinaryService;

        public async Task<bool> CreateLecture(LectureDTO lectureDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var lecture = _mapper.Map<Lecture>(lectureDto);
            if (lectureDto.Lecture != null)
                lecture.LectureUrl = await _userHelpers.AddFileAsync(lectureDto.Lecture, Folder.Lecture);
            //lecture.LectureUrl = await _cloudinaryService.UploadVideoAsync(lectureDto.Lecture);
            await _unitOfWork.Lectures.AddAsync(lecture);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteLecture(string id)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var lecture = await _unitOfWork.Lectures.FindFirstAsync(c => c.Id == id) ?? throw new Exception("lecture not found");
            var oldImgPath = lecture.LectureUrl;

            await _unitOfWork.Lectures.RemoveAsync(lecture);

            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Lecture);
                //await _cloudinaryService.DeleteVideoAsync(oldImgPath);
                return true;
            }
            return false;
        }

        public async Task<List<LectureResultDTO>> GetCourseLectures(string courseId)
        {
            var lectures = await _unitOfWork.Lectures.FindAsync(b => b.CourseId == courseId);
            var lecturesResult = _mapper.Map<IEnumerable<LectureResultDTO>>(lectures).ToList();
            return lecturesResult;
        }

        public async Task<LectureResultDTO> GetLecture(string id)
        {
            var lecture = await _unitOfWork.Lectures.FindFirstAsync(c => c.Id == id) ?? throw new Exception("lecture not found");
            var lectureResult = _mapper.Map<LectureResultDTO>(lecture);
            return lectureResult;
        }

        public async Task<int> GetNumberOfLecturesInCourse(string courseId)
        {
            var lectures = await _unitOfWork.Lectures.FindAsync(sc => sc.CourseId == courseId);
            return lectures.Count();
        }

        public async Task<bool> UpdateLecture(string id, EditLectureDTO lectureDto)
        {
            _ = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            var lecture = await _unitOfWork.Lectures.FindFirstAsync(c => c.Id == id) ?? throw new Exception("course not found");
            _mapper.Map(lectureDto, lecture);
            var oldImgPath = lecture.LectureUrl;
            if (lectureDto.Lectur != null)
            {
                lecture.LectureUrl = await _userHelpers.AddFileAsync(lectureDto.Lectur, Folder.Lecture);
                //lecture.LectureUrl = await _cloudinaryService.UploadVideoAsync(lectureDto.Lectur);
            }
            await _unitOfWork.Lectures.UpdateAsync(lecture);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Lecture);
                //await _cloudinaryService.DeleteVideoAsync(oldImgPath);
                return true;
            }
            return false;
        }
    }
}
