using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamController(IExamService examService) : ControllerBase
    {
        private readonly IExamService _examService = examService;


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPost]
        public async Task<IActionResult> CreateExam(ExamDTO examDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _examService.CreateExam(examDto);
            return result ? Ok("Created successfully") : BadRequest("Failed to create");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPut]
        public async Task<IActionResult> UpdateExam(string id, EditExamDTO exeamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _examService.UpdateExam(id, exeamDto);
            return result ? Ok("updated successfully") : BadRequest("failed to update");

        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpDelete]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var result = await _examService.DeleteExam(id);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");

        }

        [Authorize]
        [HttpGet("all-in-course")]
        public async Task<IActionResult> GetCourseExams(string courseId)
        {
            return Ok(await _examService.GetCourseExams(courseId));
        }

        [Authorize]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetExam(string id)
        {
            var course = await _examService.GetExam(id);
            return course != null ? Ok(course) : throw new Exception("Exam not found");
        }


        [Authorize]
        [HttpGet("num-of-exams")]
        public async Task<IActionResult> GetnumberOfLecturesInCourse(string courseId)
        {
            return Ok(await _examService.GetNumberOfExamsInCourse(courseId));
        }

        [Authorize]
        [HttpGet("result")]
        public async Task<IActionResult> GetExamResult(string examId)
        {
            return Ok(await _examService.GetExamResult(examId));
        }

        [Authorize]
        [HttpPost("set-result")]
        public async Task<IActionResult> SetExamResult(string examId, double examResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _examService.SetExamResult(examId, examResult);
            return result ? Ok("set successfully") : BadRequest("Failed to set");
        }
    }
}
