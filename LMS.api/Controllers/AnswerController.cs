using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ConstRoles.Teacher)]
    public class AnswerController(IAnswerSevrice answerSevrice) : ControllerBase
    {
        private readonly IAnswerSevrice _answerSevrice = answerSevrice;

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(AnswerDTO answerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _answerSevrice.CreateAnswer(answerDto);
            return result ? Ok("Created successfully") : BadRequest("Failed to create");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(string id, EditAnswerDTO editAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _answerSevrice.UpdateAnswer(id, editAnswerDto);
            return result ? Ok("updated successfully") : BadRequest("failed to update");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAnswer(string id)
        {
            var result = await _answerSevrice.DeleteAnswer(id);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");
        }
    }
}
