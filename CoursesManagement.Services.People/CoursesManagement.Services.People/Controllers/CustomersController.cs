using CoursesManagement.Common.Auth;
using CoursesManagement.Common.Dispatchers;
using CoursesManagement.Common.ErrorHandler;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Dto;
using CoursesManagement.Services.People.Messages.Commands;
using CoursesManagement.Services.People.Queries;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher, IBusPublisher busClient) : base(dispatcher, busClient)
        {
        }

        [HttpGet("customers")]
        [AdminAuth]
        public async Task<ActionResult<PagedResult<PersonDto>>> Get([FromQuery] BrowsePersons query)
            => Collection(await QueryAsync(query));

        [HttpGet("customers/{id}")]
        [AdminAuth]
        public async Task<ActionResult<PersonDto>> Get([FromRoute] GetPerson query)
            => Single(await QueryAsync(query));

        [HttpPost("customers")]
        public async Task<IActionResult> Post(CreatePerson command)
           => await SendAsync(command.Bind(c => c.Id, UserId),
               resourceId: command.Id, resource: "people");

        [AdminAuth]
        [HttpPut("customers/{id}")]
        public async Task<IActionResult> Put(UpdatePerson command)
           => await SendAsync(command.Bind(c => c.Id, UserId),
               resourceId: command.Id, resource: "people");

        [AdminAuth]
        [HttpDelete("customers/{id}")]
        public async Task<IActionResult> Delete(DeletePerson command)
           => await SendAsync(command.Bind(c => c.Id, UserId),
               resourceId: command.Id, resource: "people");
    }
}
