using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Events
{
    public class CourseJoined : IEvent
    {
        public Guid UserId { get; }
        public Guid CourseId { get; }

        public CourseJoined(Guid userId, Guid courseId)
        {
            UserId = userId;
            CourseId = courseId;
        }
    }
}
