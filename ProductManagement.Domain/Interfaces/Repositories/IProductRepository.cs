using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Common;
using System;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<PagedResult<Product>> GetPagedByFilterAsync(ProductFilter productFilter, int page, int pageSize);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        [Obsolete]
        Task<bool> RemoveAsync(int id);
    }
}