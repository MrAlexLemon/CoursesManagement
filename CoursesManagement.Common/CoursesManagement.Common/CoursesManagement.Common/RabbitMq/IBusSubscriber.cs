using CoursesManagement.Common.Messages;
using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, CoursesManagementException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, CoursesManagementException, IRejectedEvent> onError = null)
            where TEvent : IEvent;
    }
}
