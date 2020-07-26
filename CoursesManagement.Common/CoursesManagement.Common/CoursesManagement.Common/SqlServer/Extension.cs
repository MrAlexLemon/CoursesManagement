using Autofac;
using CoursesManagement.Common.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.SqlServer
{
    public static class Extension
    {
        public static void RegisterContext<TContext>(this ContainerBuilder builder)
    where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration.GetConnectionString("sql"),
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public static void AddSqlRepository<TEntity, TContext>(this ContainerBuilder builder)
            where TEntity : class, IIdentifiable
            where TContext : notnull, DbContext
            => builder.Register(ctx => new Repository<TEntity>(ctx.Resolve<TContext>()))
                .As<IRepository<TEntity>>()
                .InstancePerLifetimeScope();
    }
}
