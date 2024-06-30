using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EvaluationDTO
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public string CourseId { get; set; }
    }
}
