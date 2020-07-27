using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Queries
{
    public class GetCourse : IQuery<CourseDto>
    {
        public Guid Id { get; set; }
    }
}
