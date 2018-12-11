using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.Application;
using MyCompany.Application.Courses;
using MyCompany.Infrastructure.MessageBus;

namespace MyCompany.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyCompany();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMessageBus messageBus, ICourseService courseService)
        {
            messageBus.Listen<SignUpMessage>("course.signup", async message =>
            {
                await courseService.SignUpAsync(message.CourseId, message.Student);
            });
        }
    }
}
