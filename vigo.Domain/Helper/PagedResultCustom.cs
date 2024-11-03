using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Helper
{
    public class PagedResultCustom<T>
    {
        public List<T> Items { get; }
        public int TotalRecords { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public PagedResultCustom(List<T> items, int totalRecords, int pageIndex, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
