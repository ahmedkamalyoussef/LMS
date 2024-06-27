
namespace LMS.Data.Entities
{
    public class Student:ApplicationUser
    {
        public ICollection<StudentCourse>? StudentCourses { get; set; }=new HashSet<StudentCourse>();
    }
}
