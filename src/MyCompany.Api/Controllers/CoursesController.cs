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
        public IActionResult Get(Guid courseId)
        {
            return Ok("value");
        }

        // PUT api/courses/{guid}/signup
        [HttpPut("{courseId:guid}/signup")]
        public async Task<IActionResult> SignUpAsync(Guid courseId, [FromBody] StudentDto student)
        {
            await _courseService.SignUpAsync(courseId, student);

            return NoContent();
        }
    }
}
