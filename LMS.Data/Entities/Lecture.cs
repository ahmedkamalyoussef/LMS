namespace LMS.Data.Entities
{
    public class Lecture
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string LectureUrl { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
