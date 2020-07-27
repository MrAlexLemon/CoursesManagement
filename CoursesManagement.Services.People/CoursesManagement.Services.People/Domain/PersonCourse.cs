using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Domain
{
    public class PersonCourse
    {
        public Guid PersonId { get; private set; }
        public Person Person { get; private set; }

        public Guid CourseId { get; private set; }
        public Course Course { get; private set; }

        public Week Day { get; private set; }
        public DateTime Time { get; private set; }

        public PersonCourse(Guid personId, Guid courseId, Week day, DateTime time)
        {
            PersonId = personId;
            CourseId = courseId;
            Day = day;
            Time = time;
        }

    }

    public enum Week
    {
        Monaday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}
