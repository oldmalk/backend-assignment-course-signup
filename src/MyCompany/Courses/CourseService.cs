namespace MyCompany.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        
        public CourseService(ICourseRepository courseRepository)
        {
        }
    }
}