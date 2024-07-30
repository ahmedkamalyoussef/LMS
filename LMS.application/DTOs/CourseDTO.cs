using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class CourseDTO
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? MaterialName { get; set; }

        //public IFormFile? CourseImage { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public double Price { get; set; }

    }
}
