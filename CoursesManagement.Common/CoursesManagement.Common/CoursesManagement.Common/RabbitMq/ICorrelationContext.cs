using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.RabbitMq
{
    public interface ICorrelationContext
    {
        Guid Id { get; }
        Guid UserId { get; }
        Guid ResourceId { get; }
        DateTime CreatedAt { get; }
        int Retries { get; set; }
    }
}
