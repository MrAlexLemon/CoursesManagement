using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Commands
{
    public class JoinCourse : ICommand
    {
        public Guid UserId { get; }
        public Guid CourseId { get; }

        public DayOfWeek Day { get; }
        public DateTime Date { get; }

        public JoinCourse(Guid userId, Guid courseId, DayOfWeek day, DateTime date)
        {
            UserId = userId;
            CourseId = courseId;
            Date = date;
            Day = day;
        }
    }
}
