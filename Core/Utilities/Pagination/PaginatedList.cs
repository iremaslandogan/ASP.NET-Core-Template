using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Pagination
{
    public class PaginatedList<T>
    {
        public int Count { get; set; }
        public List<T> Items { get; set; }
    }
}
