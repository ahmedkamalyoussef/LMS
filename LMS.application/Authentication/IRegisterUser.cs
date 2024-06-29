using LMS.Data.Consts;

namespace LMS.Application.Authentication
{
    public interface IRegisterUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Govenorate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public AccountType AccountType { get; set; }
    }
}
