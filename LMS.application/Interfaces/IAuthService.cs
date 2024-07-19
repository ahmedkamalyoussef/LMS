using LMS.Application.Authentication;
using LMS.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LMS.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(RegisterUser registerUser);
        public Task<AuthModel> LoginAsync(LoginUser loginUser);
        public Task<string> LogoutAsync(string token);
        public Task<bool> ForgetPasswordAsync(string email);
        public Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword);
        public Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword);
        public Task<IdentityResult> VerifyOTPAsync(VerifyOTPRequest request);
        public Task<IdentityResult> SendOTPAsync(string email);
        public Task<AuthModel> RefreshTokenAsync(string Token);
        public Task<bool> RevokeTokenAsync(string Token);
        Task<IUserResultDTO> GetCurrentUserInfoAsync();
        public Task<bool> EditAccount(EditUserDTO userDto);
    }
}