using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using MyCompany.Application.Courses;
using MyCompany.Domain.Courses;
using MyCompany.Domain.Events;
using MyCompany.Infrastructure.MessageBus;

namespace MyCompany.Test.Application
{
    public class CourseServiceTest
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly Mock<IMessageBus> _messageBusMock;
        private readonly Mock<IEventProcessor> _eventProcessorMock;
        private readonly CourseService _courseService;

        public CourseServiceTest()
        {
            _courseRepositoryMock = _mockRepository.Create<ICourseRepository>();
            _messageBusMock = _mockRepository.Create<IMessageBus>();
            _eventProcessorMock = _mockRepository.Create<IEventProcessor>();
            _courseService = new CourseService(_courseRepositoryMock.Object, _messageBusMock.Object, _eventProcessorMock.Object);
        }

        [Theory]
        [InlineData(1)] // Tests the exact capacity
        [InlineData(10)]
        public async void SignUpAsync_WhenCapacityNotReached_ShouldSignUpStudentAndPersistCourse(uint courseCapacity)
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var courseStub = new Course(Guid.NewGuid(), teacher, courseCapacity);
            var studentDto = new StudentDto { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(courseStub.Id))
                .ReturnsAsync(courseStub);

            _eventProcessorMock
                .Setup(m => m.Process(It.IsAny<DomainEvent>()))
                .Returns(Task.CompletedTask);

            // Act
            await _courseService.SignUpAsync(courseStub.Id, studentDto);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void SignUpAsync_WhenCapacityReached_ShouldNotAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var courseStub = new Course(Guid.NewGuid(), teacher, 0);
            var studentDto = new StudentDto { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(courseStub.Id))
                .ReturnsAsync(courseStub);

            _eventProcessorMock
                .Setup(m => m.Process(It.IsAny<DomainEvent>()))
                .Returns(Task.CompletedTask);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _courseService.SignUpAsync(courseStub.Id, studentDto)
            );

            // Assert
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void GetByIdAsync_WhenCourseExists_ShouldReturnEntity()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var teacher = new Teacher { Name = "Mr. D" };
            var courseStub = new Course(courseId, teacher, 10);

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(courseStub.Id))
                .ReturnsAsync(courseStub);

            // Act
            var actualCourse = await _courseService.GetByIdAsync(courseId);

            // Assert
            _mockRepository.VerifyAll();
            actualCourse.Id.Should().Be(courseId);
        }

        [Fact]
        public async void EnqueueSignUpAsync_Always_ShouldPublishMessage()
        {
            // Arrange
            var courseId = Guid.NewGuid();
            var studentDto = new StudentDto { Name = "Fabio Fugi", Age = 42 };

            _messageBusMock
                .Setup(m => m.Publish(It.IsAny<Message>()));

            var courseService = new CourseService(_courseRepositoryMock.Object, _messageBusMock.Object, _eventProcessorMock.Object);

            // Act
            await courseService.EnqueueSignUpAsync(courseId, studentDto);

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}
