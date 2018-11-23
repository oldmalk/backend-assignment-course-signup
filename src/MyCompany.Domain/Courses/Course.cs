using System;
using System.Collections.Generic;

namespace MyCompany.Domain.Courses
{
    public class Course : IAggregationRoot
    {
        public Course(Guid id, Teacher teacher, uint capacity)
        {
            Id = id;
            Teacher = teacher;
            Capacity = capacity;
            Students = new StudentCollection();
        }

        public Course(Guid id, Teacher teacher, uint capacity, IEnumerable<Student> students)
            : this(id, teacher, capacity)
        {
        }

        public Guid Id { get; }
        public Teacher Teacher { get; }
        public uint Capacity { get; }
        public StudentCollection Students { get; }

        public int SignUp(Student student)
        {
            if (!IsSignUpAvailable())
                throw new InvalidOperationException("It's not possible sign up to a full course.");

            Students.Add(student);

            return Students.Count();
        }

        private bool IsSignUpAvailable() => Students.Count() < Capacity;
    }
}