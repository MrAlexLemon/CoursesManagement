using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Commands
{
    public class DeletePerson : ICommand
    {
        public Guid Id { get; }

        public DeletePerson(Guid id)
        {
            Id = id;
        }
    }
}
