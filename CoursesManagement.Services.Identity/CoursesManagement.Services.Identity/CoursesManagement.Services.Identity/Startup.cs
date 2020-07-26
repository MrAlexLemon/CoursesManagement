using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Identity.Domain;
using CoursesManagement.Services.Identity.EF;
using CoursesManagement.Services.Identity.Repositories;
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
            services.AddMvc()
                    .AddControllersAsServices();


            var poco = new SqlSettings();
            Configuration.GetSection("sql").Bind(poco);
            services.AddDbContext<IdentityContext>(options =>
                 options.UseSqlServer(poco.ConnectionString)
             );

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);

            //builder.RegisterContext<IdentityContext>();

            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
            builder.AddSqlRepository<RefreshToken,IdentityContext>();
            builder.AddSqlRepository<User, IdentityContext>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
