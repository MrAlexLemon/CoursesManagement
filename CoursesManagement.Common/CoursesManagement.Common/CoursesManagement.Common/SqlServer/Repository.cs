using CoursesManagement.Common.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagement.Common.SqlServer
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IIdentifiable
	{
		protected DbSet<TEntity> Entity { get; }
		protected DbContext Context { get; }

		public Repository(DbContext database)
		{
			Context = database;
			Entity = database.Set<TEntity>();
		}

		public async Task<TEntity> GetAsync(Guid id)
			=> await GetAsync(e => e.Id == id);

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
			=> await Entity.SingleOrDefaultAsync(predicate);

		public async Task<IEnumerable<TEntity>> GetAllAsync()
			=> await Entity.ToListAsync();

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
			=> await Entity.Where(predicate).ToListAsync();

		public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
				TQuery query) where TQuery : PagedQueryBase
			=> await Entity.AsQueryable().Where(predicate).PaginateAsync(query);

		public async Task AddAsync(TEntity entity)
		{
			Entity.Add(entity);
			await Context.SaveChangesAsync();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			Entity.Update(entity);
			await Context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var user = await GetAsync(id);
			Entity.Remove(user);
			await Context.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
			=> await Entity.AnyAsync(predicate);
	}
}
