using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Domain
{
    public class Person : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Country { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Completed => CompletedAt.HasValue;
        public DateTime? CompletedAt { get; private set; }

        public List<PersonCourse> PersonCourses { get; private set; }

        protected Person()
        {
            PersonCourses = new List<PersonCourse>();
        }

        public Person(Guid id, string email) : this()
        {
            Id = id;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        public void Complete(string firstName, string lastName,
            string address, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
