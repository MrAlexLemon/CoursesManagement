using CoursesManagement.Common.Messages;
using CoursesManagement.Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagement.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
