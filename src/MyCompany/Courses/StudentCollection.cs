using System.Collections.Generic;

namespace MyCompany.Courses
{
    public class StudentCollection
    {
        private readonly IList<Student> _students;
        internal StudentCollection()
        {
            _students = new List<Student>();
        }

        internal void Add(Student student)
        {
        }
    }
}