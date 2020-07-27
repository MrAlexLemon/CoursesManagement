using CoursesManagement.Services.Courses.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.EF
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
            .HasOne<Author>(a => a.Author)
            .WithMany(c => c.Courses)
            .HasForeignKey(f => f.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
            .HasOne<Subject>(a => a.Subject)
            .WithMany(c => c.Courses)
            .HasForeignKey(f=>f.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
