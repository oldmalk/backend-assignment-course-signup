using System;
using MyCompany.Infrastructure.MessageBus;

namespace MyCompany.Application.Courses
{
    public class SignUpMessage : Message
    {
        public SignUpMessage(Guid courseId, StudentDto student)
            : base("course.signup")
        {
            CourseId = courseId;
            Student = student;
        }

        public Guid CourseId { get; }
        public StudentDto Student { get; }
    }
}