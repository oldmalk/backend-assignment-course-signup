using System;
using System.Collections.Generic;

namespace MyCompany.Courses
{
    public class Course : IAggregationRoot
    {
        internal Course()
        {
            Students = new StudentCollection();
        }

        internal Course(Guid id, Teacher teacher, uint capacity, IEnumerable<Student> students)
            : this()
        {
        }

        public Guid Id { get; }
        public Teacher Teacher { get; }
        public uint Capacity { get; }
        public StudentCollection Students { get; }

        public void SignUp(Student student)
        {
        }
    }
}