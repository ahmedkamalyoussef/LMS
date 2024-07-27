using Microsoft.AspNetCore.Identity;

namespace LMS.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Govenorate { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPExpiry { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
