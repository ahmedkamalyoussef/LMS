using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EditExamDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
