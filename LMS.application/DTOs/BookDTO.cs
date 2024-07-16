using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public IFormFile Book { get; set; }
        public string CourseId { get; set; }
    }
}
