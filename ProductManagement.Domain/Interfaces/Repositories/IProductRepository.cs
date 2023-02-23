using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Common;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<PagedResult<Product>> GetPagedByFilterAsync(ProductFilter productFilter, int page, int pageSize);
        Task<bool> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> RemoveAsync(int id);
    }
}