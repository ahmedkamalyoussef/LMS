using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EditQuestionDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
