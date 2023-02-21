using AutoMapper;
using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Product> _productValidator;

        public ProductService(IProductRepository productRepository, IMapper mapper, IValidator<Product> productValidator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<PagedResponse<ProductDTO>> GetProductsAsync(ProductFilter filter, int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetPagedByFilterAsync(filter, pageNumber, pageSize);

            var productsDto = _mapper.Map<PagedResponse<ProductDTO>>(products);
            productsDto.PageNumber = pageNumber;
            productsDto.PageSize = pageSize;
            productsDto.TotalItems = products.TotalCount;

            return productsDto;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            ValidateProduct(product);

            await _productRepository.CreateAsync(product);

            return productDto;
        }

        public async Task<ProductDTO> UpdateProductAsync(int id, ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            ValidateProduct(product);

            await _productRepository.UpdateAsync(product);

            return productDto;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        private void ValidateProduct(Product product)
        {
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}