
namespace LMS.Data.Entities
{
    public class Teacher:ApplicationUser
    {
        public string? Image {  get; set; }
        public ICollection<Course>? Courses { get; set; }
        
    }
}
