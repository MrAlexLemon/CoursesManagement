using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Domain;
using CoursesManagement.Services.People.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetAsync(Guid id);
        Task<PagedResult<Person>> BrowseAsync(BrowsePersons query);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Person>> GetAllAsync();
    }
}
