using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoursesManagement.Common.Auth;
using CoursesManagement.Common.Dispatchers;
using CoursesManagement.Common.ErrorHandler;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Redis;
using CoursesManagement.Common.SqlServer;
using CoursesManagement.Common.Swagger;
using CoursesManagement.Services.Courses.Domain;
using CoursesManagement.Services.Courses.EF;
using CoursesManagement.Services.Courses.Messages.Commands;
using CoursesManagement.Services.Courses.Messages.Events;
using CoursesManagement.Services.Courses.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoursesManagement.Services.Courses
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Autofac.IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddControllers().AddNewtonsoftJson();
            services.AddJwt();
            services.AddRedis();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            var sqlOptions = new SqlSettings();
            Configuration.GetSection("sql").Bind(sqlOptions);
            services.AddDbContext<CourseContext>(options =>
                 options.UseSqlServer(sqlOptions.ConnectionString)
             );

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.AddSqlRepository<Author, CourseContext>();
            builder.AddSqlRepository<Course, CourseContext>();
            builder.AddSqlRepository<Subject, CourseContext>();
            builder.AddDispatchers();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseAuthorization();
            app.UseServiceId();
            app.UseRabbitMq()
                .SubscribeCommand<CreateCourse>(onError: (c, e) =>
                    new CreateCourseRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdateCourse>(onError: (c, e) =>
                    new UpdateCourseRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeleteCourse>(onError: (c, e) =>
                    new DeleteCourseRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<CancelCourse>(onError: (c, e) =>
                    new CancelCourseRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<FinishCourse>(onError: (c, e) =>
                    new FinishCourseRejected(c.Id, e.Message, e.Code));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
