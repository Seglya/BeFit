using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFit.Models
{
    public class PagerList<T> : List<T>

    {
        public PagerList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PagerList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source != null)
            {
                var count = source.Count();
                var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PagerList<T>(items, count, pageIndex, pageSize);
            }
            return null;
        }
    }
}