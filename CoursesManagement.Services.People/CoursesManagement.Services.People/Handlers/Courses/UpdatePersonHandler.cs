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
    public class UpdatePersonHandler : ICommandHandler<UpdatePerson>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;

        public UpdatePersonHandler(IBusPublisher busPublisher,
            IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task HandleAsync(UpdatePerson command, ICorrelationContext context)
        {
            var person = await _personRepository.GetAsync(command.Id);
            if (person.Completed)
            {
                throw new CoursesManagementException("person_data_completed",
                    $"Customer account was already created for user with id: '{command.Id}'.");
            }

            person.Complete(command.FirstName, command.LastName, command.Address, command.Country);
            await _personRepository.UpdateAsync(person);

            await _busPublisher.PublishAsync(new PersonUpdated(command.Id, command.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
        }
    }
}
