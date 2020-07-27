using CoursesManagement.Common.SqlServer;
using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Domain;
using CoursesManagement.Services.People.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepository<Person> _repository;

        public PersonRepository(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public async Task<Person> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Person person)
            => await _repository.AddAsync(person);

        public async Task UpdateAsync(Person person)
            => await _repository.UpdateAsync(person);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);

        public async Task<PagedResult<Person>> BrowseAsync(BrowsePersons query)
            => await _repository.BrowseAsync(_ => true, query);

        public async Task<IEnumerable<Person>> GetAllAsync()
            => await _repository.GetAllAsync();
    }
}
