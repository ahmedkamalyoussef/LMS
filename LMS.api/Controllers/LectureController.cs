using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureController(ILectureService lectureService) : ControllerBase
    {
        public readonly ILectureService _lectureService = lectureService;


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPost]
        public async Task<IActionResult> CreateLecture([FromForm] LectureDTO lectureDto, IFormFile Lecture)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lectureService.CreateLecture(lectureDto, Lecture);
            return result ? Ok("Created successfully") : BadRequest("Failed to create");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPut]
        public async Task<IActionResult> UpdateLecture([FromForm] EditLectureDTO lectureDto, IFormFile? Lecture)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lectureService.UpdateLecture(lectureDto.Id, lectureDto, Lecture);
            return result ? Ok("updated successfully") : BadRequest("failed to update");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpDelete]
        public async Task<IActionResult> DeleteLecture(string id)
        {
            var result = await _lectureService.DeleteLecture(id);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");

        }

        [Authorize]
        [HttpGet("all-in-course")]
        public async Task<IActionResult> GetCourseLectures(string courseId)
        {
            var courses = await _lectureService.GetCourseLectures(courseId);
            return Ok(courses);
        }

        [Authorize]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetLecture(string id)
        {
            var course = await _lectureService.GetLecture(id);
            return course != null ? Ok(course) : throw new Exception("Lecture not found");
        }


        [Authorize]
        [HttpGet("num-of-Lectures")]
        public async Task<IActionResult> GetnumberOfLecturesInCourse(string courseId)
        {
            var courses = await _lectureService.GetNumberOfLecturesInCourse(courseId);
            return Ok(courses);
        }
    }
}
