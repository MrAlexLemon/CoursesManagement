using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IRepository<Author> _repository;

        public AuthorRepository(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<Author> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Author author)
            => await _repository.AddAsync(author);

        public async Task UpdateAsync(Author author)
            => await _repository.UpdateAsync(author);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
