using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Events
{
    public class PersonUpdated : IEvent
    {
        public Guid Id { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Country { get; }
        public DateTime CreatedAt { get; }

        public PersonUpdated(Guid id, string email, string firstName,
            string lastName, string address, string country)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
