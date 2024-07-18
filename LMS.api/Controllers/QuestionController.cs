using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPost]
        public async Task<IActionResult> CreateBook(QuestionDTO questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _questionService.CreateQuestion(questionDto);
            return result ? Ok("Created successfully") : BadRequest("Failed to create");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(string id, EditQuestionDTO questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _questionService.UpdateQuestion(id, questionDto);
            return result ? Ok("updated successfully") : BadRequest("failed to update");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var result = await _questionService.DeleteQuestion(id);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");
        }

        [Authorize]
        [HttpGet("all-in-exam")]
        public async Task<IActionResult> GetExamQuestions(string examId)
        {
            return Ok(await _questionService.GetExamQuestions(examId));
        }

        [Authorize]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetQuestion(string id)
        {
            var course = await _questionService.GetQuestion(id);
            return course != null ? Ok(course) : throw new Exception("question not found");
        }


        [Authorize]
        [HttpGet("num-of-Questions")]
        public async Task<IActionResult> GetNumberOfQuestionsInExam(string examId)
        {
            return Ok(await _questionService.GetNumberOfQuestionsInExam(examId));
        }
    }
}
