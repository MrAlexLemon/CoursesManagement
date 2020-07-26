using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class RefreshTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        public RefreshTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}
