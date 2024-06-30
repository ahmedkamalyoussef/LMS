using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Data.Entities
{
    public class Course
    {
        [NotMapped]
        private static readonly Random random = new();

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? MaterialName { get; set; }
        public string? Image { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public double Price { get; set; }
        public string TeacherId { get; set; }
        public string Code { get; set; } = random.Next(10000, 100000).ToString();
        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public ICollection<Book>? Books { get; set; } = new HashSet<Book>();
        public ICollection<Lecture>? Lectures { get; set; } = new HashSet<Lecture>();
        public ICollection<Exam>? Exams { get; set; } = new HashSet<Exam>();
        public ICollection<Evaluation>? Evaluations { get; set; } = new HashSet<Evaluation>();
    }
}
