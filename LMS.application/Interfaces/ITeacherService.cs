using Microsoft.AspNetCore.Http;

namespace LMS.Application.Interfaces
{
    public interface ITeacherService
    {
        public Task<bool> EditTeacherImage(IFormFile image);
        public Task<bool> DeleteTeacherPictureAsync();
        public Task<int> GetTeachersCount();

    }
}
