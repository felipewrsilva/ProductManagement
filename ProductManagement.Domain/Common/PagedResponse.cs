using System.Collections.Generic;

namespace ProductManagement.Domain.Common
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
        public object TotalPages { get; set; }

        public static PagedResponse<T> Create(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
        {
            return new PagedResponse<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }
    }
}