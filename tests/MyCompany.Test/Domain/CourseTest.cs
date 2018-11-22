using System;
using Xunit;
using FluentAssertions;
using MyCompany.Courses;

namespace MyCompany.Test.Domain
{
    public class CourseTest
    {
        [Fact]
        public void SignUp_WhenLimitNotReached_ShouldAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 10);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            uint actualStudentsCount = 0;
            Exception actualException = null;

            // Act
            try
            {
                course.SignUp(student);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            // Assert
            actualStudentsCount.Should().Be(1);
            actualException.Should().BeNull();
        }

        [Fact]
        public void SignUp_WhenLimitReached_ShouldNotAddStudent()
        {
            // Arrange
            var teacher = new Teacher { Name = "Mr. D" };
            var course = new Course(Guid.NewGuid(), teacher, 0);
            var student = new Student { Name = "Fabio Fugi", Age = 42 };

            uint actualStudentsCount = 0;
            Exception actualException = null;

            // Act
            try
            {
                course.SignUp(student);
            }
            catch (Exception e)
            {
                actualException = e;
            }

            // Assert
            actualStudentsCount.Should().Be(0);
            actualException.Should().NotBeNull();
        }
    }
}
