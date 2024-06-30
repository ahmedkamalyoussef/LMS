using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class ExamDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CourseId { get; set; }
    }
}
