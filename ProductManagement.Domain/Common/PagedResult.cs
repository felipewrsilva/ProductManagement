using System.Collections.Generic;

namespace ProductManagement.Domain.Common
{
    public class PagedResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public PagedResult(int pageIndex, int pageSize, long totalCount, IReadOnlyList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }
    }
}