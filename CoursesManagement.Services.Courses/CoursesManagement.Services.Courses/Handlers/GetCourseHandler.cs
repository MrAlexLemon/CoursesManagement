using CoursesManagement.Common.Handlers;
using CoursesManagement.Services.Courses.Dto;
using CoursesManagement.Services.Courses.Queries;
using CoursesManagement.Services.Courses.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Handlers
{
    public sealed class GetCourseHandler : IQueryHandler<GetCourse,CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;

        public GetCourseHandler(ICourseRepository courseRepository, ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<CourseDto> HandleAsync(GetCourse query)
        {
            var course = await _courseRepository.GetAsync(query.Id);
            var subject = await _subjectRepository.GetAsync(course.SubjectId);
            return course == null ? null : new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Description = course.Description,
                OtherDetails = course.OtherDetails,
                MaxMember = course.MaxMember,
                State = course.State,
                StartTime = course.StartTime,
                EndTime = course.EndTime,
                SubjectName = subject.Name,
                Teacher = course.AuthorId
            };
        }
    }
}
