using AutoMapper;
using LMS.Application.Authentication;
using LMS.Application.DTOs;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Application.Mail;
using LMS.Data.Consts;
using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Application.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager, IMapper mapper,
        IUserHelpers userHelpers,
        IMailingService mailingService,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor
        ,IUnitOfWork unitOfWork    ) : IAuthService
    {
        #region fields
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;
        private readonly IMailingService _mailingService = mailingService;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        #endregion

        #region Registration
        public async Task<IdentityResult> RegisterAsync(RegisterUser registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);

            if (userExist != null)
            {
                if (userExist.EmailConfirmed)
                {
                    await SendOTPAsync(userExist.Email);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new IdentityError { Description = "User already exists" });
            }

            ApplicationUser user = registerUser.AccountType switch
            {
                AccountType.Student => _mapper.Map<Student>(registerUser),
                AccountType.Teacher => _mapper.Map<Teacher>(registerUser),
            };

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid account type." });
            }

            IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerUser.AccountType.ToString());

                var otp = GenerateOTP();
                user.OTP = otp;
                user.OTPExpiry = DateTime.UtcNow.AddMinutes(15);
                await _userManager.UpdateAsync(user);

                var message = new MailMessage(new[] { user.Email }, "Your OTP for Email Confirmation", $"Your OTP is: {otp}");
                _mailingService.SendMail(message);
            }

            return result;
        }



        #endregion

        #region login
        public async Task<AuthModel> LoginAsync(LoginUser loginUser)
        {
            var authModel = new AuthModel();
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (user == null)
                    return new AuthModel { Message = "user not found" };

                if (!await _userManager.CheckPasswordAsync(user, loginUser.Password))
                    return new AuthModel { Message = "Invalid password" };
                if (!user.EmailConfirmed)
                {
                    return new AuthModel { Message = "user not confirmed" };
                }

                authModel.Message = $"Welcome Back, {user.FirstName}";
                authModel.UserName = user.UserName;
                authModel.Email = user.Email;
                authModel.Token = await _userHelpers.GenerateJwtTokenAsync(user);

                authModel.IsAuthenticated = true;
                authModel.Roles = [.. (await _userManager.GetRolesAsync(user))];

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var ActiveRefreshToken = user.RefreshTokens.First(a => a.IsActive);
                    authModel.RefreshToken = ActiveRefreshToken.Token;
                    authModel.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken();
                    user.RefreshTokens.Add(refreshToken);
                    await _userManager.UpdateAsync(user);
                    authModel.RefreshToken = refreshToken.Token;
                    authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
                }
                return authModel;
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = "Invalid Authentication", Errors = new List<string> { ex.Message } };
            }
        }
        #endregion

        #region logout
        public async Task<string> LogoutAsync()
        {
            if (await _userHelpers.GetCurrentUserAsync() == null)
            {
                return "User Not Found";
            }
            await _signInManager.SignOutAsync();

            return "User Logged Out Successfully";
        }
        #endregion

        #region Refresh Token
        public async Task<AuthModel> RefreshTokenAsync(string Token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == Token));
            if (user == null)
            {
                return new AuthModel { Message = "Invalid Token" };
            }
            var refreshToken = user.RefreshTokens.Single(x => x.Token == Token);
            if (!refreshToken.IsActive)
            {
                return new AuthModel { Message = "InActive Token" };
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            var jwtToken = await _userHelpers.GenerateJwtTokenAsync(user);
            return new AuthModel
            {
                Token = jwtToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn,
                IsAuthenticated = true,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user) as List<string>,
            };
        }
        #endregion

        #region Revoke Token
        public async Task<bool> RevokeTokenAsync(string Token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == Token));
            if (user == null)
            {
                return false;
            }
            var refreshToken = user.RefreshTokens.Single(x => x.Token == Token);
            if (!refreshToken.IsActive)
            {
                return false;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }
        #endregion

        #region Password Management
        public async Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            var user = await _userHelpers.GetCurrentUserAsync();
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (user.OTP != changePassword.OTP || user.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired OTP" });

            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;

            var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            await SendOTPAsync(email);
            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (user.OTP != resetPassword.OTP || user.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired OTP" });

            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            await _userManager.UpdateAsync(user);

            return result;
        }
        #endregion

        #region OTP Management
        public async Task<IdentityResult> SendOTPAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var otp = GenerateOTP();
            user.OTP = otp;
            user.OTPExpiry = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);

            var message = new MailMessage(new[] { user.Email }, "Your OTP", $"Your OTP is: {otp}");
            _mailingService.SendMail(message);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> VerifyOTPAsync(VerifyOTPRequest verifyOTPRequest)
        {
            var user = await _userManager.FindByEmailAsync(verifyOTPRequest.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (user.OTP != verifyOTPRequest.OTP || user.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired OTP" });

            user.OTP = string.Empty;
            user.OTPExpiry = DateTime.MinValue;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return IdentityResult.Success;
        }
        #endregion

        #region private methods
        private string GenerateOTP()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[6];
                rng.GetBytes(byteArray);

                var sb = new StringBuilder();
                foreach (var byteValue in byteArray)
                {
                    sb.Append(byteValue % 10);
                }
                return sb.ToString();
            }
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddMinutes(20),
                CreatedOn = DateTime.UtcNow,
            };
        }


        #endregion

        #region Get User
        public async Task<IUserResultDTO> GetCurrentUserInfoAsync()
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("User not found.");

            IUserResultDTO result;
            if (await _userManager.IsInRoleAsync(currentUser,ConstRoles.Teacher))
                 result = _mapper.Map<TeacherResultDTO>(currentUser);

            else if (await _userManager.IsInRoleAsync(currentUser, ConstRoles.Student))
                result = _mapper.Map<StudenResultDTO>(currentUser);

            else result = _mapper.Map<AdminResultDTO>(currentUser);

            return result;
        }

        #endregion
        public async Task<bool> EditAccount(EditUserDTO userDto)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("user not found");
            try
            {
                currentUser = _mapper.Map(userDto, currentUser);
                await _unitOfWork.Users.UpdateAsync(currentUser);
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
