using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Domain
{
    public class Course : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string OtherDetails { get; private set; }
        public decimal Price { get; private set; }
        public int MaxMember { get; private set; }
        public State State { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Course(Guid id, string name, string description, string otherDetails,
            decimal price, DateTime startTime, DateTime endTime, State state, int maxMember, Guid authorId, Guid subjectId)
        {
            Id = id;
            SetName(name);
            SetPrice(price);
            SetDate(startTime, endTime);
            Description = description;
            OtherDetails = otherDetails;
            State = state;
            MaxMember = maxMember;
            AuthorId = authorId;
            SubjectId = subjectId;
        }

        public void SetState(State state)
        {
            State = state;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CoursesManagementException("empty_course_name",
                    "Course name cannot be empty.");
            }

            Name = name.Trim().ToLowerInvariant();
        }

        public void SetDate(DateTime start, DateTime finish)
        {
            if (finish.CompareTo(start)<0)
            {
                throw new CoursesManagementException("date_course_invalid",
                    "End date can not be high than start date.");
            }

            StartTime = start;
            EndTime = finish;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new CoursesManagementException("invalid_product_price",
                    "Product price cannot be zero or negative.");
            }

            Price = price;
        }
    }

    public enum State
    {
        Started = 1,
        Finished = 2,
        Cancelled = 3,
        Updated = 4,
        Deleted = 5,
        Full = 6
    }
}
