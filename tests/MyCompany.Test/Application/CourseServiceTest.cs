using System;
using Xunit;
using Moq;
using FluentAssertions;
using MyCompany.Domain.Courses;
using MyCompany.Application.Courses;

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

        [Fact]
        public void SignUp_WhenLimitNotReached_ShouldAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 10);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(course.Id))
                .ReturnsAsync(course);

            _courseRepositoryMock
                .Setup(m => m.SaveAsync(It.Is<Course>(it => it.Id == course.Id)));

            var courseService = new CourseService(_courseRepositoryMock.Object);

            // Act
            courseService.SignUp(course.Id, It.IsAny<Student>());

            // Assert
            _mockRepository.VerifyAll();
        }

        [Fact]
        public void SignUp_WhenLimitReached_ShouldNotAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 10);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            _courseRepositoryMock
                .Setup(m => m.GetByIdAsync(course.Id))
                .ReturnsAsync(course);

            var courseService = new CourseService(_courseRepositoryMock.Object);

            // Act
            courseService.SignUp(course.Id, It.IsAny<Student>());

            // Assert
            _mockRepository.VerifyAll();
        }
    }
}
