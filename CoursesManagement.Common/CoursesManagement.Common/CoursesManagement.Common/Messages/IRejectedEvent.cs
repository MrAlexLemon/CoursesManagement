using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Messages
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        string Code { get; }
    }
}
