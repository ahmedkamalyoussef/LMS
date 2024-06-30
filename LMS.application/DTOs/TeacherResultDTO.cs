namespace LMS.Application.DTOs
{
    public class TeacherResultDTO:IUserResultDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Govenorate { get; set; }
        public string Image { get; set; }

    }
}
