﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Models
{
    public class PagerList<T>: List<T>

    {
        public int PageIndex { get; private set; }
        public  int TotalPages { get; private set; }

        public PagerList (List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            this.AddRange(items);

        }
        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

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
