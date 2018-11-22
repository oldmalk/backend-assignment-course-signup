using System;
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

        public void SignUp(Guid courseId, Student student)
        {
        }
    }
}