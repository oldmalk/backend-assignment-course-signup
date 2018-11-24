using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Domain.Courses;

namespace MyCompany.Infrastructure.MongoDb
{
    public class MongoCourseRepository : ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}