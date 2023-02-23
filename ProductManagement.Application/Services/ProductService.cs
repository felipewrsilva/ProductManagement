using AutoMapper;
using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
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

        public async Task<PagedResponse<ProductDTO>> GetAsync(ProductFilter filter, int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetPagedByFilterAsync(filter, pageNumber, pageSize);

            var productsDto = _mapper.Map<PagedResponse<ProductDTO>>(products);
            productsDto.PageNumber = pageNumber;
            productsDto.PageSize = pageSize;

            return productsDto;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            Validate(product);

            await _productRepository.AddAsync(product);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            Validate(product);

            await _productRepository.UpdateAsync(product);

            return productDto;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            product.Situation = ProductSituation.Inactive;
            return await _productRepository.UpdateAsync(product);
        }

        private void Validate(Product product)
        {
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}