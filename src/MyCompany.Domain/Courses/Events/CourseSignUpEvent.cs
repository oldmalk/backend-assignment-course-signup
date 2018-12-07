using System;

namespace MyCompany.Domain.Courses.Events
{
    public class CourseSignUpEvent : DomainEvent
    {
        private readonly Course _course;
        private readonly Student _student;

        public CourseSignUpEvent(Course course, Student student, DateTime date) : base(date)
        {
            _course = course;
            _student = student;
        }

        public override void Process()
        {
            _course.SignUp(_student);
        }
    }
}