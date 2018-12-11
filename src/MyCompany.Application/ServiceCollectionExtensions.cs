using Microsoft.Extensions.DependencyInjection;
using MyCompany.Application.Courses;
using MyCompany.Domain.Courses;
using MyCompany.Infrastructure.MessageBus;
using MyCompany.Infrastructure.MongoDb;

namespace MyCompany.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyCompany(this IServiceCollection services) => services
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<ICourseRepository, MongoCourseRepository>()
            .AddScoped<IMessageBus, RabbitMessageBus>();

    }
}