using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Queries
{
    public class BrowseCourses : PagedQueryBase, IQuery<PagedResult<CourseDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}
