namespace LMS.Data.Entities
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? MaterialName { get; set; }
        public string? Image { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }=new HashSet<StudentCourse>();
    }
}
