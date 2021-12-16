using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class FreeSqlRepositoryExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T, K>(this IBaseRepository<T> repository, QueryHelper<T> qg, Expression<Func<T, K>> orderBy, bool descending = true) where T : class
        {
            var where = await qg.GetExpression();
            var (page, size) = await qg.GetPageInfo();

            var rows = await repository
                .Where(where)
                .Count(out var total)
                .OrderByIf(true, orderBy, descending)
                .Page(page, size)
                .ToListAsync();

            return new PagedList<T>() { Page = page, PageSize = size, Rows = rows, Total = total };
        }
        public static async Task<PagedList<K>> ToPagedListAsync<T, K, M>(this IBaseRepository<T> repository, QueryHelper<T> qg, Expression<Func<T, K>> selector, Expression<Func<T, M>> orderBy, bool descending = true) where T : class
        {
            var where = await qg.GetExpression();
            var (page, size) = await qg.GetPageInfo();

            var rows = await repository
                .Where(where)
                .Count(out var total)
                .OrderByIf(true, orderBy, descending)
                .Page(page, size)

                .ToListAsync(selector);

            return new PagedList<K>() { Page = page, PageSize = size, Rows = rows, Total = total };
        }
    }
}
