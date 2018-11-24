using System;
using Xunit;
using Moq;
using FluentAssertions;
using MyCompany.Domain.Courses;
using MyCompany.Application.Courses;
using System.Threading.Tasks;

namespace MyCompany.Test.Application
{
    public class CourseServiceTest
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        private readonly Mock<ICourseRepository> _courseRepositoryMock;

        public CourseServiceTest()
        {
            _courseRepositoryMock = _mockRepository.Create<ICourseRepository>();
        }

        [Theory]
        [InlineData(1)] // Tests the exact capacity
        [InlineData(10)]
        public async void SignUpAsync_WhenCapacityNotReached_ShouldSignUpStudentAndPersistCourse(uint courseCapacity)
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, courseCapacity);
            var studentDto = new StudentDto { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(course.Id))
                .ReturnsAsync(course);

            _courseRepositoryMock
                .Setup(m => m.SaveAsync(It.Is<Course>(it => it.Id == course.Id)))
                .Returns(Task.CompletedTask);

            var courseService = new CourseService(_courseRepositoryMock.Object);

            // Act
            await courseService.SignUpAsync(course.Id, studentDto);

            // Assert
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void SignUpAsync_WhenCapacityReached_ShouldNotAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 0);
            var studentDto = new StudentDto { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(course.Id))
                .ReturnsAsync(course);

            var courseService = new CourseService(_courseRepositoryMock.Object);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                courseService.SignUpAsync(course.Id, studentDto)
            );

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}
