using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Services.People.Domain;
using CoursesManagement.Services.People.Messages.Events;
using CoursesManagement.Services.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Handlers.Identity
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IPersonRepository _personRepository;

        public SignedUpHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
        {
            var customer = new Person(@event.UserId, @event.Email);
            await _personRepository.AddAsync(customer);
        }
    }
}
