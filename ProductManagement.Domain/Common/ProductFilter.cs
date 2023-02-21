using System;

namespace ProductManagement.Domain.Common
{
    public class ProductFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCnpj { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}