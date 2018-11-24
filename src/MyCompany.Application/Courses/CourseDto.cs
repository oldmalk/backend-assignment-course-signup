using System;
using System.Collections.Generic;

namespace MyCompany.Application.Courses
{
    public class CourseDto
    {
        public CourseDto(Guid id, IEnumerable<StudentDto> students = null)
        {
            this.Id = id;
            this.Students = students;
        }

        public Guid Id { get; }
        public IEnumerable<StudentDto> Students { get; }
    }
}