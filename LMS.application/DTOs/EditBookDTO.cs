using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs
{
    public class EditBookDTO
    {
        public string Title { get; set; }
        public IFormFile Book { get; set; }
    }
}
