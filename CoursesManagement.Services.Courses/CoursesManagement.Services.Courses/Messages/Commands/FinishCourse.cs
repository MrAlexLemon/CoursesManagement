using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Messages.Commands
{
    public class FinishCourse : ICommand
    {
        public Guid Id { get; }

        public FinishCourse(Guid id)
        {
            Id = id;
        }
    }
}
