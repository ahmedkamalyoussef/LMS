using LMS.Data.Entities;
using LMS.Domain.Consts;
using Microsoft.AspNetCore.Http;

namespace LMS.Application.Helpers
{
    public interface IUserHelpers
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<string> AddFileAsync(IFormFile file, Folder folder);
        Task<bool> DeleteFileAsync(string imagePath, Folder folder);
    }
}
