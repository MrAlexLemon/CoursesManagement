using CoursesManagement.Services.Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Dto
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public decimal Price { get; set; }
        public int MaxMember { get; set; }
        public State State { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubjectName { get; set; }
        public Guid Teacher { get; set; }
    }
}
