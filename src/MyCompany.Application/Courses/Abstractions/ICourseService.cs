using System;
using System.Threading.Tasks;
using MyCompany.Domain.Courses;

namespace MyCompany.Application.Courses.Abstractions
{
    public interface ICourseService
    {
         Task SignUpAsync(Guid courseId, StudentDto studentDto);
    }
}