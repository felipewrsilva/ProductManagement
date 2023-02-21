using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Common;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDTO>> GetProductsAsync(ProductFilter filter, int pageNumber, int pageSize);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(int id, ProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}