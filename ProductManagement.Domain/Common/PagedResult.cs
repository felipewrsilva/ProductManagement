using System.Collections.Generic;

namespace ProductManagement.Domain.Common
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }

        public static PagedResult<T> Create(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
        {
            return new PagedResult<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };
        }
    }
}