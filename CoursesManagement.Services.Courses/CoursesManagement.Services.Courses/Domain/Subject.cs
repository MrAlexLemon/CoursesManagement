using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Domain
{
    public class Subject : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public List<Course> Courses { get; set; }

        public Subject(Guid id, string name)
        {
            Id = id;
            SetName(name);
            Courses = new List<Course>();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CoursesManagementException("empty_subject_name",
                    "Subject name cannot be empty.");
            }

            Name = name.Trim().ToLowerInvariant();
        }
    }
}
