using CoursesManagement.Services.Identity.Domain;
using CoursesManagement.Services.Identity.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            // Some logic
            return Ok();
        }
    }
}
