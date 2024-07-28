using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Consts;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Services
{
    public class TeacherService(IUnitOfWork unitOfWork, IUserHelpers userHelpers, CloudinaryService cloudinaryService) : ITeacherService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserHelpers _userHelpers = userHelpers;
        private readonly CloudinaryService _cloudinaryService = cloudinaryService;


        public async Task<bool> DeleteTeacherPictureAsync()
        {
            Teacher user = (Teacher)await _userHelpers.GetCurrentUserAsync();
            if (user == null) return false;
            var oldImgPath = user.Image;
            user.Image = null;
            await _unitOfWork.Users.UpdateAsync(user);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Profile);
                //await _cloudinaryService.DeleteImageAsync(oldImgPath);
                return true;
            }
            return false;
        }

        public async Task<bool> EditTeacherImage(IFormFile image)
        {
            Teacher user = (Teacher)await _userHelpers.GetCurrentUserAsync();
            if (user == null) return false;
            var oldImgPath = user.Image;
            user.Image = await _userHelpers.AddFileAsync(image, Folder.Profile);

            //user.Image = await _cloudinaryService.UploadImageAsync(image);

            await _unitOfWork.Users.UpdateAsync(user);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                if (oldImgPath != null)
                    await _userHelpers.DeleteFileAsync(oldImgPath, Folder.Profile);
                //await _cloudinaryService.DeleteImageAsync(oldImgPath);
                return true;
            }
            return false;
        }

        public async Task<int> GetTeachersCount()
        {
            return await _unitOfWork.Teachers.CountAsync();
        }
    }
}
