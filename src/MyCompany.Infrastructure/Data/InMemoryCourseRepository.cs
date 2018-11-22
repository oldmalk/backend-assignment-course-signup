using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Courses;

namespace MyCompany.Infrastructure.MongoDb
{
    public class InMemoryCourseRepository : ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> SaveAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}