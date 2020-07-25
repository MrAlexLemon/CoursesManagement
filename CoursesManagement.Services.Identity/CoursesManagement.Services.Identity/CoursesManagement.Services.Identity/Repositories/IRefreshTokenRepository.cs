using CoursesManagement.Common.Types;
using CoursesManagement.Services.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Repositories
{
    public interface IRefreshTokenRepository<TEntity> where TEntity : IIdentifiable
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
