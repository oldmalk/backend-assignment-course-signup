using System;
using System.Threading.Tasks;
using MyCompany.Domain.Courses;

namespace MyCompany.Application.Courses
{
    public interface ICourseService
    {
        Task SignUpAsync(Guid courseId, StudentDto studentDto);
        Task EnqueueSignUpAsync(Guid courseId, StudentDto studentDto);
        Task<CourseDto> GetByIdAsync(Guid courseId);
    }
}