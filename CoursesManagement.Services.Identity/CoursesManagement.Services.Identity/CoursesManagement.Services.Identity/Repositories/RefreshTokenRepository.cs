using CoursesManagement.Common.SqlServer;
using CoursesManagement.Services.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IRepository<RefreshToken> _repository;

        public RefreshTokenRepository(IRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
            => await _repository.AddAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.UpdateAsync(token);
    }
}
