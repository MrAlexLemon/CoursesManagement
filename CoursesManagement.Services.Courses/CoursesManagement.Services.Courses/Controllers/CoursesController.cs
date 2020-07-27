using CoursesManagement.Common.Auth;
using CoursesManagement.Common.Dispatchers;
using CoursesManagement.Common.ErrorHandler;
using CoursesManagement.Common.RabbitMq;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Dto;
using CoursesManagement.Services.Courses.Messages.Commands;
using CoursesManagement.Services.Courses.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Controllers
{
    [Route("[controller]")]
    public class CoursesController : BaseController
    {
        public CoursesController(IDispatcher dispatcher, IBusPublisher busClient)
            : base(dispatcher, busClient)
        {
        }

        [AdminAuth]
        [HttpGet("people")]
        public async Task<ActionResult<PagedResult<CourseDto>>> Get([FromQuery] BrowseCourses query)
            => Collection(await QueryAsync(query));

        [AdminAuth]
        [HttpGet("people/{id}")]
        public async Task<ActionResult<CourseDto>> GetAsync([FromRoute] GetCourse query)
            => Single(await QueryAsync(query));


        [HttpPost("people")]
        public async Task<IActionResult> Post(CreateCourse command)
            => await SendAsync(command.BindId(c => c.Id),
                resourceId: command.Id, resource: "course");
        
        [AdminAuth]
        [HttpPut("people/{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCourse command)
            => await SendAsync(command.Bind(c => c.Id, id),
                resourceId: command.Id, resource: "course");

        [AdminAuth]
        [HttpDelete("people/{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await SendAsync(new DeleteCourse(id));
    }
}
