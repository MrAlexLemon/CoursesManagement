using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.EF
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.Id);*/

            //TODO  configure constraints.
        }
    }
}
