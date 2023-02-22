using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces.Repositories;

namespace ProductManagement.UnitTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IValidator<Product>> _productValidatorMock;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _productValidatorMock = new Mock<IValidator<Product>>();
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object, _productValidatorMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            var product = new Product
            {
                Id = productId,
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

            var productDto = new ProductDTO
            {
                Id = product.Id,
                Description = product.Description,
                ManufactureDate = product.ManufactureDate,
                ExpirationDate = product.ExpirationDate,
                Situation = product.Situation,
                Supplier = new SupplierDTO
                {
                    Id = product.Supplier.Id,
                    Description = product.Supplier.Description,
                    Cnpj = product.Supplier.Cnpj
                }
            };

            _productRepositoryMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDto);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(productDto.Id, result.Id);
            Assert.AreEqual(productDto.Description, result.Description);
            Assert.AreEqual(productDto.ManufactureDate, result.ManufactureDate);
            Assert.AreEqual(productDto.ExpirationDate, result.ExpirationDate);
            Assert.AreEqual(productDto.Situation, result.Situation);
            Assert.AreEqual(productDto.Supplier.Id, result.Supplier.Id);
            Assert.AreEqual(productDto.Supplier.Description, result.Supplier.Description);
            Assert.AreEqual(productDto.Supplier.Cnpj, result.Supplier.Cnpj);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _productRepositoryMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetAsync_ReturnsPagedResponse()
        {
            // Arrange
            var filter = new ProductFilter();
            var pageNumber = 1;
            var pageSize = 10;


            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Description = "Product 1", Situation = ProductSituation.Active },
                new Product { Id = 2, Description = "Product 2", Situation = ProductSituation.Active },
                new Product { Id = 3, Description = "Product 3", Situation = ProductSituation.Active },
            };

            _productRepositoryMock
                .Setup(x => x.GetPagedByFilterAsync(filter, pageNumber, pageSize))
                .ReturnsAsync(PagedResult<Product>.Create(expectedProducts, 3, pageNumber, pageSize));

            var expectedResponse = PagedResponse<ProductDTO>.Create(
                expectedProducts.Select(x => new ProductDTO { Id = x.Id, Description = x.Description, Situation = x.Situation }),
                pageSize,
                3,
                pageNumber);

            // Act
            var result = await _productService.GetAsync(filter, pageNumber, pageSize);

            // Assert
            Assert.IsInstanceOf<PagedResponse<ProductDTO>>(result);
            Assert.AreEqual(3, result.Items.Count());
            Assert.AreEqual(pageNumber, result.PageNumber);
            Assert.AreEqual(pageSize, result.PageSize);
        }

        [Test]
        public async Task GetAsync_ReturnsPagedResponseWithCorrectPageSize()
        {
            // Arrange
            int pageSize = 10;
            int pageNumber = 1;

            var products = new List<Product>();
            for (int i = 1; i <= 20; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Description = $"Product {i}",
                    Situation = ProductSituation.Active,
                    ManufactureDate = DateTime.Now.AddDays(-i),
                    ExpirationDate = DateTime.Now.AddDays(i),
                    Supplier = new Supplier
                    {
                        Id = i,
                        Description = $"Supplier {i}",
                        Cnpj = $"00.000.000/{i:0000}"
                    }
                });
            }

            var productFilter = new ProductFilter();

            // Act
            var result = await _productService.GetAsync(productFilter, pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(pageSize);
            result.PageSize.Should().Be(pageSize);
            result.PageNumber.Should().Be(pageNumber);
            result.TotalPages.Should().Be((int)Math.Ceiling(products.Count / (double)pageSize));
            result.TotalItems.Should().Be(products.Count);
        }

        //[Test]
        //public async Task GetAsync_ReturnsPagedResponseWithCorrectPageNumber()
        //{

        //}

        //[Test]
        //public async Task GetAsync_ReturnsPagedResponseWithCorrectData()
        //{

        //}

        //[Test]
        //public async Task GetByIdAsync_ReturnsProductDTO()
        //{

        //}

        //[Test]
        //public async Task GetByIdAsync_ReturnsNullWhenNotFound()
        //{

        //}

        //[Test]
        //public async Task CreateAsync_ThrowsValidationException_WhenDateOfManufactureIsGreaterThanExpirationDate()
        //{

        //}

        //[Test]
        //public async Task CreateAsync_CreatesProductAndReturnsProductDTO()
        //{

        //}

        //[Test]
        //public async Task UpdateAsync_ThrowsValidationException_WhenDateOfManufactureIsGreaterThanExpirationDate()
        //{

        //}

        //[Test]
        //public async Task UpdateAsync_ReturnsNullWhenProductNotFound()
        //{

        //}

        //[Test]
        //public async Task UpdateAsync_UpdatesProductAndReturnsProductDTO()
        //{

        //}

        //[Test]
        //public async Task DeleteAsync_ReturnsFalseWhenProductNotFound()
        //{

        //}

        //[Test]
        //public async Task DeleteAsync_LogicallyDeletesProductAndReturnsTrue()
        //{

        //}
    }
}