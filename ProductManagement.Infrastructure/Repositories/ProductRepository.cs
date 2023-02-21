using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetByFilterAsync(ProductFilter productFilter)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(productFilter.Description))
                query = query.Where(p => p.Description.Contains(productFilter.Description));

            if (productFilter.Id.HasValue)
                query = query.Where(p => p.Id == productFilter.Id);

            if (productFilter.SupplierId.HasValue)
                query = query.Where(p => p.SupplierId == productFilter.SupplierId.Value);

            return await query.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetBySupplierIdAsync(int supplierId)
        {
            return await _dbContext.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
        }

        public async Task<PagedResult<Product>> GetPagedByFilterAsync(ProductFilter productFilter, int pageIndex, int pageSize)
        {
            var query = _dbContext.Products.AsQueryable();  

            if (!string.IsNullOrEmpty(productFilter.Description))
                query = query.Where(p => p.Description.Contains(productFilter.Description));

            if (productFilter.Id.HasValue)
                query = query.Where(p => p.Id == productFilter.Id);

            if (productFilter.SupplierId.HasValue)
                query = query.Where(p => p.SupplierId == productFilter.SupplierId.Value);

            var totalCount = await query.CountAsync();

            var products = await query.Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new PagedResult<Product>(pageIndex, pageSize, totalCount, products);
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}