using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CoursesManagement.Common.Auth;
using CoursesManagement.Common.ErrorHandler;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Redis;
using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Identity.Domain;
using CoursesManagement.Services.Identity.EF;
using CoursesManagement.Services.Identity.Repositories;
using CoursesManagement.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoursesManagement.Services.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            /*services.AddMvc()
                    .AddControllersAsServices();*/

            services.AddCustomMvc();
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
            services.AddDbContext<IdentityContext>(options =>
                 options.UseSqlServer(sqlOptions.ConnectionString)
             );


            services.AddSingleton<IJwtHandler, JwtHandler>();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();

            //builder.RegisterContext<IdentityContext>();

            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
            builder.AddSqlRepository<RefreshToken,IdentityContext>();
            builder.AddSqlRepository<User, IdentityContext>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();
            app.UseRabbitMq();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
