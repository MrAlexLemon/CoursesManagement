using CoursesManagement.Common.Handlers;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Dto;
using CoursesManagement.Services.Courses.Queries;
using CoursesManagement.Services.Courses.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Handlers
{
    public sealed class BrowseCoursesHandler : IQueryHandler<BrowseCourses, PagedResult<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public BrowseCoursesHandler(ICourseRepository courseRepository)
            => _courseRepository = courseRepository;

        public async Task<PagedResult<CourseDto>> HandleAsync(BrowseCourses query)
        {
            var pagedResult = await _courseRepository.BrowseAsync(query);
            var courses = pagedResult.Items.Select(p => new CourseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OtherDetails = p.OtherDetails,
                Price = p.Price,
                MaxMember = p.MaxMember,
                State = p.State,
                StartTime = p.StartTime,
                EndTime = p.StartTime
            }).ToList();

            return PagedResult<CourseDto>.From(pagedResult, courses);
        }
    }
}
