using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ConstRoles.Student)]

    public class EvaluationController(IEvaluationService evaluationService) : ControllerBase
    {
        private readonly IEvaluationService _evaluationService = evaluationService;

    }
}
