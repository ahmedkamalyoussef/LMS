using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = ConstRoles.Student)]

    public class EvaluationController(IEvaluationService evaluationService) : ControllerBase
    {
        private readonly IEvaluationService _evaluationService = evaluationService;


        [HttpPost]
        public async Task<IActionResult> AddEvaluationAsync(EvaluationDTO evaluationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _evaluationService.AddEvaluationAsync(evaluationDTO);
            return result ? Ok("added successfully") : BadRequest("Failed to add");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvaluationAsync(string courseId)
        {
            var result = await _evaluationService.DeleteEvaluationAsync(courseId);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");
        }
    }
}
