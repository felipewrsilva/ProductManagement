using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.UnitTests.Builders;
using System;

namespace ProductManagement.UnitTests
{
    internal class ProductDTOBuilder
    {
        private ProductDTO _productDTO;

        public ProductDTOBuilder()
        {
            _productDTO = new ProductDTO
            {
                Id = 1,
                Description = "ProductDTO 1",
                ManufactureDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(30),
                Situation = ProductSituation.Active,
                Supplier = new SupplierDTO
                {
                    Id = 1,
                    Description = "SupplierDTO 1",
                    Cnpj = "12345678901234"
                }
            };
        }

        public ProductDTOBuilder WithId(int id)
        {
            _productDTO.Id = id;
            return this;
        }

        public ProductDTO Build()
        {
            return _productDTO;
        }
    }
}