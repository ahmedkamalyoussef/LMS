namespace LMS.Application.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string? MaterialName { get; set; }

        public string Level { get; set; }
        public string Semester { get; set; }
        public double Price { get; set; }

    }
}
