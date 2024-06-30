using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class TeacherService(IUnitOfWork unitOfWork, IUserHelpers userHelpers) : ITeacherService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserHelpers _userHelpers = userHelpers;


        public async Task<bool> DeleteTeacherPictureAsync()
        {
            Teacher user = (Teacher) await _userHelpers.GetCurrentUserAsync();
            if (user == null) return false;
            user.Image = null;
            await _unitOfWork.Users.UpdateAsync(user);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> EditTeacherImage(string imagePath)
        {
            Teacher user = (Teacher) await _userHelpers.GetCurrentUserAsync();
            if (user == null) return false;
            user.Image = imagePath;
            await _unitOfWork.Users.UpdateAsync(user);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<int> GetTeachersCount()
        {
            return await _unitOfWork.Teachers.CountAsync();
        }
    }
}
