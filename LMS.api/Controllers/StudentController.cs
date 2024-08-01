﻿using LMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;


        [HttpGet("count")]
        public async Task<IActionResult> GetStudentCount()
        {
            var count = await _studentService.GetStudentsCount();
            return Ok(count);
        }
    }
}
