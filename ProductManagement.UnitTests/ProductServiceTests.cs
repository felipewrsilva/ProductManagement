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
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.UnitTests.Builders;
using FluentValidation.Results;
using ProductManagement.Domain.Enums;

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
            var product = new ProductBuilder().Build();
            var productDTO = new ProductDTOBuilder().WithId(product.Id).Build();

            _productRepositoryMock.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetByIdAsync(product.Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(productDTO);
        }

        [Test]
        public async Task GetAsync_ReturnsPagedResponse()
        {
            // Arrange
            const int NumberOfProducts = 3;
            const int PageNumber = 1;
            const int PageSize = 10;

            var filter = new ProductFilter();

            var expectedProducts = CreateProducts(NumberOfProducts);

            _productRepositoryMock
                .Setup(x => x.GetPagedByFilterAsync(filter, PageNumber, PageSize))
                .ReturnsAsync(PagedResult<Product>.Create(expectedProducts, expectedProducts.Count, PageNumber, PageSize));

            var expectedResponse = CreateExpectedResponse(expectedProducts, PageSize, PageNumber);

            _mapperMock.Setup(x => x.Map<PagedResponse<ProductDTO>>(It.IsAny<PagedResult<Product>>()))
                .Returns(expectedResponse);

            // Act
            var result = await _productService.GetAsync(filter, PageNumber, PageSize).ConfigureAwait(false);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetAsync_ReturnsPagedResponseWithCorrectPageSize()
        {
            // Arrange
            const int PageSize = 10;
            const int PageNumber = 1;
            const int NumberOfProducts = 20;

            var expectedProducts = CreateProducts(NumberOfProducts);
            var expectedResponse = CreateExpectedResponse(expectedProducts, PageSize, PageNumber);

            _productRepositoryMock
                .Setup(x => x.GetPagedByFilterAsync(It.IsAny<ProductFilter>(), PageNumber, PageSize))
                .ReturnsAsync(PagedResult<Product>.Create(expectedProducts.Take(PageSize), expectedProducts.Count, PageNumber, PageSize));

            _mapperMock.Setup(x => x.Map<PagedResponse<ProductDTO>>(It.IsAny<PagedResult<Product>>()))
                .Returns(expectedResponse);

            // Act
            var result = await _productService.GetAsync(new ProductFilter(), PageNumber, PageSize).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(PageSize);
            result.PageSize.Should().Be(PageSize);
            result.PageNumber.Should().Be(PageNumber);
            result.TotalPages.Should().Be((int)Math.Ceiling(expectedProducts.Count / (double)PageSize));
            result.TotalItems.Should().Be(expectedProducts.Count);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateProduct_WhenValidProductIsProvided()
        {
            // Arrange
            var product = new ProductBuilder().Build();
            var productDTO = new ProductDTOBuilder().WithId(product.Id).Build();

            _mapperMock.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            _productRepositoryMock.Setup(x => x.CreateAsync(product)).ReturnsAsync(true);
            _productValidatorMock.Setup(v => v.Validate(product)).Returns(new ValidationResult());

            // Act
            var result = await _productService.CreateAsync(productDTO).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(productDTO);
            _productRepositoryMock.Verify(r => r.CreateAsync(product), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateProduct_WhenValidProductIsProvided()
        {
            // Arrange
            var productDTO = new ProductDTOBuilder().Build();
            var product = new ProductBuilder().WithId(productDTO.Id).Build();

            _mapperMock.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            _productRepositoryMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync(true);
            _productValidatorMock.Setup(v => v.Validate(product)).Returns(new ValidationResult());

            // Act
            var result = await _productService.UpdateAsync(productDTO).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(productDTO);
            _productRepositoryMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Test]
        public async Task DeactivateAsync_ShouldMarkProductAsInactive_WhenValidProductIdIsProvided()
        {
            // Arrange
            var product = new ProductBuilder().Build();
            _productRepositoryMock.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
            _productRepositoryMock.Setup(r => r.UpdateAsync(It.Is<Product>(p => p.Id == product.Id)))
                .ReturnsAsync(true);

            // Act
            var result = await _productService.DeactivateAsync(product.Id);

            // Assert
            result.Should().BeTrue();
            _productRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Product>(p => p.Id == product.Id && p.Situation == ProductSituation.Inactive)), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 1;
            _productRepositoryMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetByIdAsync(productId).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
        }

        private static List<Product> CreateProducts(int numberOfProducts)
        {
            var products = new List<Product>();

            for (int i = 1; i <= numberOfProducts; i++)
                products.Add(new ProductBuilder().WithId(i).Build());

            return products;
        }

        private static PagedResponse<ProductDTO> CreateExpectedResponse(List<Product> products, int pageSize, int pageNumber)
        {
            return PagedResponse<ProductDTO>.Create(
                items: products.Take(pageSize).Select(x => new ProductDTOBuilder().WithId(x.Id).Build()),
                totalItems: products.Count,
                pageNumber: pageNumber,
                pageSize: pageSize
            );
        }
    }
}