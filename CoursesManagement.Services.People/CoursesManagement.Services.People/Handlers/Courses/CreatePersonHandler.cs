using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
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
    public class CreatePersonHandler : ICommandHandler<CreatePerson>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;

        public CreatePersonHandler(IBusPublisher busPublisher,
            IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task HandleAsync(CreatePerson command, ICorrelationContext context)
        {
            var customer = await _personRepository.GetAsync(command.Id);

            if (customer == null)
            {
                await _personRepository.AddAsync(new Person(command.Id, command.Email));
                await _busPublisher.PublishAsync(new PersonCreated(command.Id, customer.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
                /*throw new CoursesManagementException("person_not_exist",
                    $"Customer account was not already created for user with id: '{command.Id}'.");*/
            }


            if (customer.Completed)
            {
                throw new CoursesManagementException("person_data_completed",
                    $"Customer account was already created for user with id: '{command.Id}'.");
            }

            customer.Complete(command.FirstName, command.LastName, command.Address, command.Country);
            await _personRepository.UpdateAsync(customer);

            await _busPublisher.PublishAsync(new PersonCreated(command.Id, customer.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
        }
    }
}
