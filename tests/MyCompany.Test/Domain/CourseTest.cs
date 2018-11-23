using System;
using Xunit;
using FluentAssertions;
using MyCompany.Domain.Courses;

namespace MyCompany.Test.Domain
{
    public class CourseTest
    {
        [Theory]
        [InlineData(1)] // Tests the exact capacity
        [InlineData(10)]
        public void SignUp_WhenCapacityNotReached_ShouldSignUpStudent(uint courseCapacity)
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, courseCapacity);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            int actualStudentsCount = 0;

            // Act
            actualStudentsCount = course.SignUp(student);

            // Assert
            actualStudentsCount.Should().Be(1);
        }

        [Fact]
        public void SignUp_WhenCapacityReached_ShouldNotSignUpStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 0);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            int actualStudentsCount = 0;

            // Act
            Assert.Throws<InvalidOperationException>(() =>
                actualStudentsCount = course.SignUp(student)
            );

            // Assert
            actualStudentsCount.Should().Be(0);
        }
    }
}
