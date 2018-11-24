using System;
using System.Threading.Tasks;
using MyCompany.Domain.Courses;
using MyCompany.Infrastructure.MessageBus;

namespace MyCompany.Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMessageBus _messageBus;

        public CourseService(ICourseRepository courseRepository, IMessageBus messageBus)
        {
            _courseRepository = courseRepository;
            _messageBus = messageBus;
        }

        public Task EnqueueSignUpAsync(Guid courseId, StudentDto studentDto)
        {
            var message = new SignUpMessage(courseId, studentDto);
            return Task.Run(() => _messageBus.Publish(message));
        }

        public async Task<CourseDto> GetByIdAsync(Guid courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            return new CourseDto(course.Id);
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