using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public static class AsyncFake
    {
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            await Task.Delay(0);
            return source.FirstOrDefault(predicate);
        }

        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source)
        {
            await Task.Delay(0);
            return source.FirstOrDefault();
        }

        public static async Task<IEnumerable<TSource>> WhereAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            await Task.Delay(0);
            return source.Where(predicate);
        }
    }
}
