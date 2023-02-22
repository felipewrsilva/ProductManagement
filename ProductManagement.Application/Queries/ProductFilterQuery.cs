using ProductManagement.Domain.Common;

namespace ProductManagement.Application.Queries
{
    public class ProductFilterQuery
    {
        public ProductFilter ProductFilter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ProductFilterQuery(ProductFilter productFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            ProductFilter = productFilter ?? new ProductFilter();
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}