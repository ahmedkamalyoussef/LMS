using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs
{
    public class LectureDTO
    {
        public string Name { get; set; }
        public IFormFile Lecture { get; set; }
        public string CourseId { get; set; }
    }
}
