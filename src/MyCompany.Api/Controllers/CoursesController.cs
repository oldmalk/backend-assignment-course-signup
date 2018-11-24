using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Application.Courses;
using MyCompany.Application.Courses.Abstractions;

namespace MyCompany.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET api/courses
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/courses/5
        [HttpGet("{courseId:guid}")]
        public async Task<IActionResult> Get(Guid courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);

            return Ok(course);
        }

        // POST api/courses/{guid}/signup
        [HttpPost("{courseId:guid}/signup")]
        public async Task<IActionResult> SignUpAsync(Guid courseId, [FromBody] StudentDto student)
        {
            await _courseService.SignUpAsync(courseId, student);

            return NoContent();
        }

        // POST api/v2/courses/{guid}/signup
        [HttpPost("v2/{courseId:guid}/signup")]
        public async Task<IActionResult> EnqueueSignUpAsync(Guid courseId, [FromBody] StudentDto student)
        {
            await _courseService.EnqueueSignUpAsync(courseId, student);

            return NoContent();
        }
    }
}
