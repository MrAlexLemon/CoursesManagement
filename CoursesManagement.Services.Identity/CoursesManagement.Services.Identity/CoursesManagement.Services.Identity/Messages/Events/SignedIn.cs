﻿using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Events
{
    public class SignedIn : IEvent
    {
        public Guid UserId { get; }

        public SignedIn(Guid userId)
        {
            UserId = userId;
        }
    }
}
