using CoursesManagement.Common.SqlServer;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.Courses.Domain;
using CoursesManagement.Services.Courses.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IRepository<Course> _repository;

        public CourseRepository(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task<Course> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(p => p.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await _repository.ExistsAsync(p => p.Name == name.ToLowerInvariant());

        public async Task<PagedResult<Course>> BrowseAsync(BrowseCourses query)
            => await _repository.BrowseAsync(p =>
                p.Price >= query.PriceFrom && p.Price <= query.PriceTo, query);

        public async Task AddAsync(Course course)
            => await _repository.AddAsync(course);

        public async Task UpdateAsync(Course course)
            => await _repository.UpdateAsync(course);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Course>> GetAllAsync()
            => await _repository.GetAllAsync();
    }
}
