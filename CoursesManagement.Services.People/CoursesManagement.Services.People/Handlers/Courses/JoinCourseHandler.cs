using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Services.People.Domain;
using CoursesManagement.Services.People.Messages.Commands;
using CoursesManagement.Services.People.Messages.Events;
using CoursesManagement.Services.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Handlers.Courses
{
    public class JoinCourseHandler : ICommandHandler<JoinCourse>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;

        public JoinCourseHandler(IBusPublisher busPublisher,
            IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task HandleAsync(JoinCourse command, ICorrelationContext context)
        {
            var user = await _personRepository.GetAsync(command.UserId);
            user.PersonCourses.Add(new PersonCourse(command.UserId, command.CourseId,Week.Sunday,DateTime.UtcNow));
            await _personRepository.UpdateAsync(user);

            await _busPublisher.PublishAsync(new CourseJoined(command.UserId,command.CourseId), context);
        }
    }
}
