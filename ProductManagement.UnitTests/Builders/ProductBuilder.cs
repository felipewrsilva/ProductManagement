using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.UnitTests.Builders
{
    public class ProductBuilder
    {
        private readonly Product _product;

        public ProductBuilder()
        {
            _product = new Product
            {
                Id = 1,
                Description = "Product 1",
                ManufactureDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(30),
                Situation = ProductSituation.Active,
                Supplier = new Supplier
                {
                    Id = 1,
                    Description = "Supplier 1",
                    Cnpj = "36.294.739/0001-09"
                }
            };
        }

        public ProductBuilder WithId(int id)
        {
            _product.Id = id;
            _product.Description = $"Product {id}";
            _product.Supplier.Description = $"Supplier {id}";
            _product.Supplier.Cnpj = $"36.294.739/{id:0000}-09";
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}
