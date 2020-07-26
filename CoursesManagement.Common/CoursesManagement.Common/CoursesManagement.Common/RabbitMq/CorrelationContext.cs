using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.RabbitMq
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid ResourceId { get; }
        public DateTime CreatedAt { get; }
        public int Retries { get; set; }

        public CorrelationContext()
        {
        }

        private CorrelationContext(Guid id)
        {
            Id = id;
        }

        private CorrelationContext(Guid id, Guid userId, Guid resourceId, int retries)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Retries = retries;
            CreatedAt = DateTime.UtcNow;
        }

        public static ICorrelationContext Empty
            => new CorrelationContext();

        public static ICorrelationContext FromId(Guid id)
            => new CorrelationContext(id);

        public static ICorrelationContext From<T>(ICorrelationContext context)
            => Create<T>(context.Id, context.UserId, context.ResourceId);

        public static ICorrelationContext Create<T>(Guid id, Guid userId, Guid resourceId)
            => new CorrelationContext(id, userId, resourceId, 0);
    }
}
