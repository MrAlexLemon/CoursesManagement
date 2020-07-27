using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Messages.Events
{
    [MessageNamespace("courses")]
    public class CourseCancelled : IEvent
    {
        public Guid Id { get; }

        public CourseCancelled(Guid id)
        {
            Id = id;
        }
    }
}
