using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Pagination
{
    public class PaginatedListCreator<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int Count { get; set; }

        public PaginatedListCreator(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            Count = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            Console.WriteLine(source.ToList());
            var count = source.Count();
            // if (pageIndex == 0 || pageSize == 0)
            if (pageSize == 0)
                return new PaginatedList<T> { Count = count, Items = source.ToList() };

            if (pageIndex == 0) pageIndex = 1;

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var x = new PaginatedListCreator<T>(items, count, pageIndex, pageSize);
            return new PaginatedList<T>
            {
                Count = count,
                Items = x
            };
        }
    }
}
