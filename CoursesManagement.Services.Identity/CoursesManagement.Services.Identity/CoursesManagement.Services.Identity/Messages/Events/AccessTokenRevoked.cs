using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class AccessTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        public AccessTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}
