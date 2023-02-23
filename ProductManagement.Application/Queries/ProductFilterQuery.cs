using ProductManagement.Domain.Common;

namespace ProductManagement.Application.Queries
{
    public class ProductFilterQuery
    {
        public ProductFilter ProductFilter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ProductFilterQuery()
        {
            ProductFilter = new ProductFilter();
            PageNumber = 1;
            PageSize = 10;
        }

        public ProductFilterQuery(ProductFilter productFilter, int pageNumber, int pageSize)
        {
            ProductFilter = productFilter;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}