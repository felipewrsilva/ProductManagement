using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Common;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDTO>> GetAsync(ProductFilter filter, int pageNumber, int pageSize);
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateAsync(ProductDTO productDto);
        Task<bool> DeactivateAsync(int id);
    }
}