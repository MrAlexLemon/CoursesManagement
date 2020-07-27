using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Events
{
    [MessageNamespace("courses")]
    public class CourseDeleted : IEvent
    {
        public Guid Id { get; }

        public CourseDeleted(Guid id)
        {
            Id = id;
        }
    }
}
