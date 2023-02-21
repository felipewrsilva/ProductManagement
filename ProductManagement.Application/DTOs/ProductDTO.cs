using ProductManagement.Domain.Common;
using System;

namespace ProductManagement.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ProductSituation Situation { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}