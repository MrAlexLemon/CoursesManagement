using CoursesManagement.Common.Dispatchers;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Dto;
using CoursesManagement.Services.Courses.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoursesManagement.Services.Courses.Controllers
{
    [Route("[controller]")]
    public class CoursesController : BaseController
    {
        public CoursesController(IDispatcher dispatcher)
            : base(dispatcher)
        {
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<PagedResult<CourseDto>>> Get([FromQuery] BrowseCourses query)
            => Collection(await QueryAsync(query));


        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetAsync([FromRoute] GetCourse query)
            => Single(await QueryAsync(query));
    }
}
