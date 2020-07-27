using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Messages.Commands;
using CoursesManagement.Services.Courses.Messages.Events;
using CoursesManagement.Services.Courses.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Handlers
{
    public sealed class DeleteCourseHandler : ICommandHandler<DeleteCourse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IBusPublisher _busPublisher;

        public DeleteCourseHandler(
            ICourseRepository courseRepository,
            IBusPublisher busPublisher)
        {
            _courseRepository = courseRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteCourse command, ICorrelationContext context)
        {
            if (!await _courseRepository.ExistsAsync(command.Id))
            {
                throw new CoursesManagementException("course_not_found",
                    $"Course with id: '{command.Id}' was not found.");
            }

            await _courseRepository.DeleteAsync(command.Id);
            await _busPublisher.PublishAsync(new CourseDeleted(command.Id), context);
        }
    }
}
