using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogLullaby.BLL.Infrastructure
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> items, int page = 0, int pageSize = 10)
        {
            return items.Skip((page) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> items, ref int pageCount, int page = 0, int pageSize = 10)
        {
            pageCount = (int)Math.Ceiling((double)items.Count() / pageSize);
            return items.Skip((page) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> items, int page = 0, int pageSize = 10)
        {
            return items.Skip((page) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> items, ref int pageCount, int page = 0, int pageSize = 10)
        {
            pageCount = (int)Math.Ceiling((double)items.Count() / pageSize);
            return items.Skip((page) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> PagingByDescending<T>(this IEnumerable<T> items, int page = 0, int pageSize = 10)
        {
            int  pagesCount = (int)Math.Ceiling((double)items.Count() / pageSize);
            if (pagesCount < page)
                return items.Take(0);
            if(pagesCount == page)
                return items.Take(items.Count() - ((pagesCount - 1) * pageSize));
            return items.Skip(((pagesCount - 1 - page) * pageSize) - (items.Count() - ((pagesCount - 1) * pageSize))).Take(pageSize);
        }
    }
}
