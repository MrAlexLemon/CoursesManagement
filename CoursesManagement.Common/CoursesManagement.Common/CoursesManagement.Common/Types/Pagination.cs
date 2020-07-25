using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Common.Types
{
    public static class Pagination
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> table, PagedQueryBase query)
            => await table.PaginateAsync(query.Page, query.Results);

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> table,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var isEmpty = await table.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResult<T>.Empty;
            }
            var totalResults = await table.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);
            var data = await table.Limit(page, resultsPerPage).ToListAsync();

            return PagedResult<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> table, PagedQueryBase query)
            => table.Limit(query.Page, query.Results);

        public static IQueryable<T> Limit<T>(this IQueryable<T> table,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var skip = (page - 1) * resultsPerPage;
            var data = table.Skip(skip)
                .Take(resultsPerPage);

            return data;
        }
    }
}
