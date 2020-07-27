using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Messages.Commands;
using CoursesManagement.Services.People.Messages.Events;
using CoursesManagement.Services.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Handlers.Courses
{
    public class DeletePersonHandler : ICommandHandler<DeletePerson>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;

        public DeletePersonHandler(IBusPublisher busPublisher,
            IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task HandleAsync(DeletePerson command, ICorrelationContext context)
        {
            var person = await _personRepository.GetAsync(command.Id);
            if (person == null)
                throw new CoursesManagementException("person_invalid_id", $"Person account doesnt exist with id: '{command.Id}'.");
            
            await _personRepository.DeleteAsync(command.Id);
            
            await _busPublisher.PublishAsync(new PersonDeleted(command.Id), context);
        }
    }
}
