using LMS.Data.Entities;
using LMS.Domain.Consts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Application.Helpers
{
    internal class UserHelpers(IConfiguration config, UserManager<ApplicationUser> userManager
            , IHttpContextAccessor contextAccessor
            , IWebHostEnvironment webHostEnvironment) : IUserHelpers
    {
        #region fields
        private IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IConfiguration _config = config;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        #endregion

        #region methods
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var currentUser = _contextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(currentUser);
        }

        public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenExpiration = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region file handling
        //public async Task<string> AddFileAsync(IFormFile file, Folder folder)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return string.Empty;
        //    }

        //    string rootPath = _webHostEnvironment.WebRootPath;
        //    var user = await GetCurrentUserAsync() ?? throw new("user not found");
        //    string userName = user.UserName;

        //    string profileFolderPath = Path.Combine(rootPath, userName, folder.ToString());
        //    if (!Directory.Exists(profileFolderPath))
        //    {
        //        Directory.CreateDirectory(profileFolderPath);
        //    }

        //    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //    string filePath = Path.Combine(profileFolderPath, fileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }
        //    return $"/{userName}/{folder}/{fileName}";
        //}

        public async Task<string> AddFileAsync(IFormFile file, Folder folder)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            string rootPath = _webHostEnvironment.WebRootPath;
            var user = await GetCurrentUserAsync() ?? throw new("user not found");
            string userName = user.UserName;

            string profileFolderPath = Path.Combine(rootPath, userName, folder.ToString());
            if (!Directory.Exists(profileFolderPath))
            {
                Directory.CreateDirectory(profileFolderPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(profileFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the absolute path 
            return filePath;
        }

        public async Task<bool> DeleteFileAsync(string filePath, Folder folder)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return true;
            }

            string rootPath = _webHostEnvironment.WebRootPath;
            var user = await GetCurrentUserAsync();
            string userName = user.UserName;

            //if (!filePath.StartsWith($"/{userName}/{folder}/"))
            //{
            //    throw new ArgumentException("Invalid file path.", nameof(filePath));
            //}

            string fullFilePath = Path.Combine(rootPath, filePath.TrimStart('/'));

            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
                return true;
            }
            else
            {
                throw new FileNotFoundException("File not found.", fullFilePath);
            }

        }
        #endregion
    }
}
