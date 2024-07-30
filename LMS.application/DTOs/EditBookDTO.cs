using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EditBookDTO
    {
        [Required]
        public string Id { get; set; }
        public string Title { get; set; }
        public IFormFile Book { get; set; }
    }
}
