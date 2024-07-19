using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Data.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookService.CreateBook(bookDto);
            return result ? Ok("Created successfully") : BadRequest("Failed to create");
        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpPut]
        public async Task<IActionResult> UpdateBook(string id, EditBookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookService.UpdateBook(id, bookDto);
            return result ? Ok("updated successfully") : BadRequest("failed to update");

        }


        [Authorize(Roles = ConstRoles.Teacher)]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var result = await _bookService.DeleteBook(id);
            return result ? Ok("deleted successfully") : BadRequest("failed to delete");

        }

        [Authorize]
        [HttpGet("all-in-course")]
        public async Task<IActionResult> GetCourseBooks(string courseId)
        {
            return Ok(await _bookService.GetCourseBooks(courseId));
        }

        [Authorize]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetBook(string id)
        {
            var course = await _bookService.GetBook(id);
            return course != null ? Ok(course) : throw new Exception("book not found");
        }


        [Authorize]
        [HttpGet("num-of-books")]
        public async Task<IActionResult> GetnumberOfBooksInCourse(string courseId)
        {
            return Ok(await _bookService.GetNumberOfBooksInCourse(courseId));
        }
    }
}
