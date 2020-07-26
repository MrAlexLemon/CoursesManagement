using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; }

        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
