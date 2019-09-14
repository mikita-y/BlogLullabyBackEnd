using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogLullaby.BLL.Infrastructure
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> items, int page = 0, int pageSize = 10)
        {
            return items.Skip((page) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> items, int page = 0, int pageSize = 10)
        {
            return items.Skip((page) * pageSize).Take(pageSize);
        }
    }
}
