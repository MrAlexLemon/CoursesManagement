using CoursesManagement.Services.People.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.EF
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People  { get; set; }
        public DbSet<Course> Courses { get; set; }

        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonCourse>()
            .HasKey(t => new { t.PersonId, t.CourseId, t.Time, t.Day });

            modelBuilder.Entity<PersonCourse>()
                .HasOne(sc => sc.Person)
                .WithMany(s => s.PersonCourses)
                .HasForeignKey(sc => sc.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.PersonCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
