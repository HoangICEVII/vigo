using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Helper
{
    public class PagedResultCustom<T>
    {
        public IEnumerable<T> Items { get; }
        public int TotalPages { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public PagedResultCustom(IEnumerable<T> items, int totalPages, int pageIndex, int pageSize)
        {
            Items = items;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
