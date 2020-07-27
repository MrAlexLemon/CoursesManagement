using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Messages.Commands
{
    public class DeleteCourse : ICommand
    {
        public Guid Id { get; }

        public DeleteCourse(Guid id)
        {
            Id = id;
        }
    }
}
