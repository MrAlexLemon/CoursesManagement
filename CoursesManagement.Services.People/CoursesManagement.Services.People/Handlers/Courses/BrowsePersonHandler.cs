using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Dto;
using CoursesManagement.Services.People.Queries;
using CoursesManagement.Services.People.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Handlers.Courses
{
    public class BrowsePersonHandler : IQueryHandler<BrowsePersons, PagedResult<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public BrowsePersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PagedResult<PersonDto>> HandleAsync(BrowsePersons query)
        {
            var pagedResult = await _personRepository.BrowseAsync(query);
            var customers = pagedResult.Items.Select(c => new PersonDto
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Country = c.Country,
                CreatedAt = c.CreatedAt,
                Completed = c.Completed
            });

            return PagedResult<PersonDto>.From(pagedResult, customers);
        }
    }
}
