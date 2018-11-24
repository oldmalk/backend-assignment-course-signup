using System;
using System.Threading.Tasks;
using MyCompany.Application.Courses.Abstractions;
using MyCompany.Domain.Courses;

namespace MyCompany.Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task SignUpAsync(Guid courseId, StudentDto studentDto)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            var student = new Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age
            };

            course.SignUp(student);

            await _courseRepository.SaveAsync(course);
        }
    }
}