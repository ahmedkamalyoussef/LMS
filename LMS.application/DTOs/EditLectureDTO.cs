using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EditLectureDTO
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public IFormFile Lectur { get; set; }
    }
}
