
namespace LMS.Data.Entities
{
    public class Student:ApplicationUser
    {
        public ICollection<StudentCourse>? StudentCourses { get; set; }=new HashSet<StudentCourse>();
        public ICollection<ExamResult>? ExamResults { get; set; } = new HashSet<ExamResult>();

    }
}
