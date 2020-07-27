using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Domain
{
    public class Author : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public List<Course> Courses { get; set; }

        public Author(Guid id, string email, string firstName, string lastName)
        {
            Id = id;
            SetEmail(email);
            SetFirstName(firstName);
            SetSecondName(lastName);
            Courses = new List<Course>();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new CoursesManagementException("empty_email_author",
                    "Email cannot be empty.");
            }

            Email = email.Trim().ToLowerInvariant();
        }

        public void SetFirstName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CoursesManagementException("empty_first_name_author",
                    "First name cannot be empty.");
            }

            FirstName = name.Trim().ToLowerInvariant();
        }

        public void SetSecondName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CoursesManagementException("empty_last_name_author",
                    "Last name cannot be empty.");
            }

            LastName = name.Trim().ToLowerInvariant();
        }
    }
}
