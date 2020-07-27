using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IRepository<Subject> _repository;

        public SubjectRepository(IRepository<Subject> repository)
        {
            _repository = repository;
        }

        public async Task<Subject> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Subject subject)
            => await _repository.AddAsync(subject);

        public async Task UpdateAsync(Subject subject)
            => await _repository.UpdateAsync(subject);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
