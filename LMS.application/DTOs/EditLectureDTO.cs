using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs
{
    public class EditLectureDTO
    {
        public string Name { get; set; }
        public IFormFile Lectur { get; set; }
    }
}
