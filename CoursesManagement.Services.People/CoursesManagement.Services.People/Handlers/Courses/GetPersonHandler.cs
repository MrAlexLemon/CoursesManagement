using CoursesManagement.Common.Handlers;
using CoursesManagement.Services.People.Dto;
using CoursesManagement.Services.People.Queries;
using CoursesManagement.Services.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Handlers.Courses
{
    public class GetPersonHandler : IQueryHandler<GetPerson, PersonDto>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonDto> HandleAsync(GetPerson query)
        {
            var customer = await _personRepository.GetAsync(query.Id);

            return customer == null ? null : new PersonDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Country = customer.Country,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
