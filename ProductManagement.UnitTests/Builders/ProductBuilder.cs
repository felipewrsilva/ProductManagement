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
                    Cnpj = "12345678901234"
                }
            };
        }

        public ProductBuilder WithId(int id)
        {
            _product.Id = id;
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}
