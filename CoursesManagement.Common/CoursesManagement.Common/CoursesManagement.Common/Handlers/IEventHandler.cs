using CoursesManagement.Common.Messages;
using CoursesManagement.Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagement.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}
