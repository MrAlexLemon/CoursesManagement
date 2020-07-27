using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Domain;
using CoursesManagement.Services.Courses.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<PagedResult<Course>> BrowseAsync(BrowseCourses query);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Course>> GetAllAsync();
    }
}
