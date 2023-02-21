using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Common;
using System.Collections.Generic;
using System;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetBySupplierIdAsync(int supplierId);
        Task<IEnumerable<Product>> GetByFilterAsync(ProductFilter productFilter);
        Task<PagedResult<Product>> GetPagedByFilterAsync(ProductFilter productFilter, int page, int pageSize);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}