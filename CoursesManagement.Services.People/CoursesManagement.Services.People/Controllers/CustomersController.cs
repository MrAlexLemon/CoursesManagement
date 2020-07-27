using CoursesManagement.Common.Dispatchers;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Dto;
using CoursesManagement.Services.People.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<PersonDto>>> Get([FromQuery] BrowsePersons query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get([FromRoute] GetPerson query)
            => Single(await QueryAsync(query));
    }
}
