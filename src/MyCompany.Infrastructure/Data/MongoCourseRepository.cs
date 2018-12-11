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
            var courseId = new Guid("f2a992fd-1c0e-431c-a585-e41f1a0b5faa");
            var teacher = new Teacher { Name = "My teacher's name" };
            var course = new Course(
                courseId,
                teacher,
                capacity: 20
            );

            return Task.FromResult(course);
        }

        public Task SaveAsync(Course course)
        {
            Console.WriteLine($"*** Course with ID {course.Id} was saved. ***");

            return Task.CompletedTask;
        }
    }
}