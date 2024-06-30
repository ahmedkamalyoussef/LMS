namespace LMS.Application.DTOs
{
    public class CourseResultDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MaterialName { get; set; }
        public string Image { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string TeacherId { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public bool IsEnrolled { get; set; }
        public double Evaluation { get; set; }
        public TeacherResultDTO Teacher { get; set; }
    }
}
