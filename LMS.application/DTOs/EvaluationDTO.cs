using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class EvaluationDTO
    {
        [Range(0, 5)]
        [Required]
        public int Value { get; set; }
        [Required]
        public string CourseId { get; set; }
    }
}
