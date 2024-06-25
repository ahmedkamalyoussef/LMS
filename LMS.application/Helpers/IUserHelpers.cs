using LMS.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Helpers
{
    public interface IUserHelpers
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetCurrentUserAsync();
        //Task<string> AddFileAsync(IFormFile file, string folderName);
        //Task<bool> DeleteFileAsync(string imagePath, string folderName);
    }
}
