using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.Domain.Courses
{
    public interface ICourseRepository
    {
        Task<Course> SaveAsync(Course course);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(Guid id);
    }
}