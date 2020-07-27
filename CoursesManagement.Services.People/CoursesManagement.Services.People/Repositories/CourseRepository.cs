using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.People.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Repositories
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

        public async Task AddAsync(Course course)
            => await _repository.AddAsync(course);

        public async Task UpdateAsync(Course course)
            => await _repository.UpdateAsync(course);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
