using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        private readonly ICourseService _courseService = courseService;

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.CreateCourse(courseDto);
            return result ? Ok("created Sucsessfully") : BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(string id, CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.UpdateCourse(id, courseDto);
            return result ? Ok("updated Sucsessfully") : BadRequest("failed to update");

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var result = await _courseService.DeleteCourse(id);
            return result ? Ok("deleted Sucsessfully") : BadRequest("failed to delete");

        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudentInCourse(string studentEmail,string courseCode)
        {
            var result = await _courseService.EnrollingStudentInCourse(studentEmail, courseCode);
            return result ? Ok("student has been added Sucsessfully") : BadRequest("failed to add student");

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetCoursesByCrateria(string carteria)
        {
            var courses = await _courseService.SearchForCources(carteria);
            return Ok(courses);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetCourse(string id)
        {
            var course = await _courseService.GetCourse(id);
            return course != null ?  Ok(course) : throw new Exception("course not found") ;
        }

        [HttpGet("teacher-courses")]
        public async Task<IActionResult> GetCoursesByTeacherId(string teacherId)
        {
            var courses = await _courseService.GetCoursesByTeacherId(teacherId);
            return Ok(courses);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetNumberOfCourses()
        {
            var count = await _courseService.GetNumberOfCourses();
            return Ok(count);
        }

        [HttpGet("students-count-in-course")]
        public async Task<IActionResult> GetStudentCountInCourse(string courseId)
        {
            var count = await _courseService.GetStudentCountInCourse(courseId);
            return Ok(count);
        }
    }
}
