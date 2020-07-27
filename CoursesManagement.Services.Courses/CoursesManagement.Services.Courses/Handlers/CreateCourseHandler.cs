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
    public sealed class CreateCourseHandler : ICommandHandler<CreateCourse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IBusPublisher _busPublisher;

        public CreateCourseHandler(
            ICourseRepository courseRepository,
            IBusPublisher busPublisher,
            ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _busPublisher = busPublisher;
            _subjectRepository = subjectRepository;
        }

        public async Task HandleAsync(CreateCourse command, ICorrelationContext context)
        {
            if (command.Price < 0)
            {
                throw new CoursesManagementException("invalid_course_price",
                    "Product quantity cannot be negative.");
            }

            if (await _courseRepository.ExistsAsync(command.Name))
            {
                throw new CoursesManagementException("course_already_exists",
                    $"Course: '{command.Name}' already exists.");
            }

            var subject = await _subjectRepository.GetAsync(command.SubjectId);


            var course = new Course(command.Id, command.Name,
                command.Description, command.OtherDetails,command.Price,command.StartTime,
                command.EndTime,State.Started,30,command.AuthorId,command.SubjectId);
            await _courseRepository.AddAsync(course);
            await _busPublisher.PublishAsync(new CourseCreated(command.Id, command.Name,
                command.Description,command.OtherDetails,command.Price,command.StartTime,command.EndTime, command.Status,command.MaxMember, subject.Name), context);
        }
    }
}
