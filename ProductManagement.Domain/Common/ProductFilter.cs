using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.Domain.Common
{
    public class ProductFilter
    {
        public int? ProductId { get; set; }
        public string ProductDescription { get; set; }
        public ProductSituation? ProductSituation { get; set; }
        public DateTime? ProductManufactureDate { get; set; }
        public DateTime? ProductExpirationDate { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ProductFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}