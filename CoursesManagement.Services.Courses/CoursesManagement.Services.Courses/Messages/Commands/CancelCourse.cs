using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Messages.Commands
{
    public class CancelCourse : ICommand
    {
        public Guid Id { get; }

        public CancelCourse(Guid id)
        {
            Id = id;
        }
    }
}
