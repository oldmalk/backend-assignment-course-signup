using System;
using System.Threading.Tasks;
using MyCompany.Domain.Courses;

namespace MyCompany.Application.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task SignUpAsync(Guid courseId, Student student)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            course.SignUp(student);

            await _courseRepository.SaveAsync(course);
        }
    }
}