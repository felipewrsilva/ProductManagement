using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Situation = ProductSituation.Active;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public ProductSituation Situation { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}