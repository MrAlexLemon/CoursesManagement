using CoursesManagement.Services.Courses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Courses.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetAsync(Guid id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Guid id);
    }
}
