using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class AccessTokenRefreshed : IEvent
    {
        public Guid UserId { get; }

        public AccessTokenRefreshed(Guid userId)
        {
            UserId = userId;
        }
    }
}
