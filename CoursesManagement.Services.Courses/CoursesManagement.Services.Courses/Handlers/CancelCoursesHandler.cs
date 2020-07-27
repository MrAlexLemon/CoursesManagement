using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Domain;
using CoursesManagement.Services.Courses.Messages.Commands;
using CoursesManagement.Services.Courses.Messages.Events;
using CoursesManagement.Services.Courses.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Handlers
{
    public class CancelCoursesHandler : ICommandHandler<CancelCourse>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CancelCoursesHandler> _logger;

        public CancelCoursesHandler(IBusPublisher busPublisher,
            ICourseRepository courseRepository,
            ILogger<CancelCoursesHandler> logger)
        {
            _busPublisher = busPublisher;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CancelCourse command, ICorrelationContext context)
        {
            _logger.LogInformation($"Cancelling a course: '{command.Id}'");
            if (!await _courseRepository.ExistsAsync(command.Id))
            {
                throw new CoursesManagementException("course_not_found",
                    $"Course with id: '{command.Id}' was not found.");
            }

            var course = await _courseRepository.GetAsync(command.Id);
            course.SetState(State.Cancelled);
            await _courseRepository.UpdateAsync(course);
            _logger.LogInformation($"Cancelled a course: '{command.Id}'");
            await _busPublisher.PublishAsync(new CourseCancelled(command.Id), context);
        }
    }
}
