using System;
using System.Threading.Tasks;
using MyCompany.Application.Events;
using MyCompany.Domain.Courses;
using MyCompany.Domain.Courses.Events;
using MyCompany.Domain.Events;
using MyCompany.Infrastructure.MessageBus;

namespace MyCompany.Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMessageBus _messageBus;
        private readonly IEventProcessor _eventProcessor;

        public CourseService(ICourseRepository courseRepository, IMessageBus messageBus, IEventProcessor eventProcessor)
        {
            _courseRepository = courseRepository;
            _messageBus = messageBus;
            _eventProcessor = eventProcessor;
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

            var courseSignUpEvent = new CourseSignUpEvent(course, student, DateTime.Now);
            _eventProcessor.Process(courseSignUpEvent);
        }
    }
}