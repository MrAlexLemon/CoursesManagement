using CoursesManagement.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Domain
{
    public class Course : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int MaxMember { get; private set; }

        public State State { get; private set; }

        public List<PersonCourse> PersonCourses { get; private set; }

        protected Course()
        {
            PersonCourses = new List<PersonCourse>();
        }

        public Course(Guid id, string name, decimal price, int quantity, State state) : this()
        {
            Id = id;
            Name = name;
            Price = price;
            MaxMember = quantity;
            State = state;
        }

        public void SetPrice(int price)
        {
            if (price < 0)
            {
                throw new CoursesManagementException("invalid_course_price",
                    "Course price cannot be negative.");
            }

            Price = price;
        }

        public void SetMaxMember(int memberCount)
        {
            if (memberCount < 0)
            {
                throw new CoursesManagementException("invalid_course_countOfMember",
                    "Course count of members cannot be negative.");
            }

            MaxMember = memberCount;
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
