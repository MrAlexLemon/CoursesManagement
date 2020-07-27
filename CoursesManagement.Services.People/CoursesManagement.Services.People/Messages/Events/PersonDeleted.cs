using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Events
{
    public class PersonDeleted : IEvent
    {
        public Guid Id { get; }

        public PersonDeleted(Guid id)
        {
            Id = id;
        }
    }
}
