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
using CoursesManagement.Services.People.Domain;
using CoursesManagement.Services.People.EF;
using CoursesManagement.Services.People.Messages.Commands;
using CoursesManagement.Services.People.Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoursesManagement.Services.People
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Autofac.IContainer Container { get; private set; }


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
            services.AddDbContext<PeopleContext>(options =>
                 options.UseSqlServer(sqlOptions.ConnectionString)
             );

            //services.RegisterServiceForwarder<IProductsService>("products-service");


            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddSqlRepository<Person, PeopleContext>();
            builder.AddSqlRepository<Course, PeopleContext>();

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
                .SubscribeCommand<CreatePerson>(onError: (c, e) =>
                    new CreatePersonRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdatePerson>(onError: (c, e) =>
                    new UpdatePersonRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeletePerson>(onError: (c, e) =>
                    new DeletePersonRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<JoinCourse>(onError: (c, e) =>
                    new JoinCourseRejected(c.UserId, e.Message, e.Code))
                .SubscribeEvent<SignedUp>(@namespace: "identity")
                .SubscribeEvent<CourseCreated>(@namespace: "courses")
                .SubscribeEvent<CourseUpdated>(@namespace: "courses")
                .SubscribeEvent<CourseDeleted>(@namespace: "courses")
                .SubscribeEvent<CourseCancelled>(@namespace: "courses")
                .SubscribeEvent<CourseFinished>(@namespace: "courses");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
