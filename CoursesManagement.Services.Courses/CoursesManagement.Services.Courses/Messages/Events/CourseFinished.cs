using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Messages.Events
{
    public class CourseFinished : IEvent
    {
        public Guid Id { get; }

        public CourseFinished(Guid id)
        {
            Id = id;
        }
    }
}
