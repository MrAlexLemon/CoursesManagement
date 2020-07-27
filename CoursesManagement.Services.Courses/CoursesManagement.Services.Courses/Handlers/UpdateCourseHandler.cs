using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Domain;
using CoursesManagement.Services.Courses.Messages.Commands;
using CoursesManagement.Services.Courses.Messages.Events;
using CoursesManagement.Services.Courses.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Handlers
{
    public sealed class UpdateCourseHandler : ICommandHandler<UpdateCourse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ISubjectRepository _subjectRepository;

        public UpdateCourseHandler(
            ICourseRepository courseRepository,
            IBusPublisher busPublisher,
            ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _busPublisher = busPublisher;
            _subjectRepository = subjectRepository;
        }

        public async Task HandleAsync(UpdateCourse command, ICorrelationContext context)
        {
            var course = await _courseRepository.GetAsync(command.Id);
            if (course == null)
            {
                throw new CoursesManagementException("course_not_found",
                    $"Course with id: '{command.Id}' was not found.");
            }

            var subject = await _subjectRepository.GetAsync(command.SubjectId);
            var newCourse = new Course(command.Id, command.Name,
                command.Description, command.OtherDetails, command.Price, command.StartTime,
                command.EndTime, State.Started, 30, command.AuthorId, command.SubjectId);
            await _courseRepository.UpdateAsync(newCourse);

            await _busPublisher.PublishAsync(new CourseUpdated(command.Id, command.Name,
                command.Description, command.OtherDetails, command.Price, command.StartTime, 
                command.EndTime, command.State, command.MaxMember, subject.Name), context);
        }
    }
}
