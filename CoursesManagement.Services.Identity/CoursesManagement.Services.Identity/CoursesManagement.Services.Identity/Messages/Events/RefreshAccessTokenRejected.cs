﻿using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class RefreshAccessTokenRejected : IRejectedEvent
    {
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        public RefreshAccessTokenRejected(Guid userId, string reason, string code)
        {
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}
