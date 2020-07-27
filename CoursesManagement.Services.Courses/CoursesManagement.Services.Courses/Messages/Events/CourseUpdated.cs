using CoursesManagement.Common.Messages;
using CoursesManagement.Services.Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Messages.Events
{
    public class CourseUpdated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string OtherDetails { get; }
        public decimal Price { get; }
        public int MaxMember { get; }
        public State State { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string SubjectName { get; }

        public CourseUpdated(Guid id, string name, string description, string otherDetails,
            decimal price, DateTime startTime, DateTime endTime, State state, int maxMember, string subjectName)
        {
            Id = id;
            Name = name;
            Price = price;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            OtherDetails = otherDetails;
            State = state;
            MaxMember = maxMember;
            SubjectName = subjectName;
        }
    }
}
