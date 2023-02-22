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

        public async Task<bool> CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<PagedResult<Product>> GetPagedByFilterAsync(ProductFilter productFilter, int pageIndex, int pageSize)
        {
            var query = _dbContext.Products.AsQueryable();

            if (productFilter.ProductId.HasValue)
                query = query.Where(p => p.Id == productFilter.ProductId);

            if (productFilter.ProductSituation.HasValue)
                query = query.Where(p => p.Situation == productFilter.ProductSituation.Value);

            if (productFilter.ProductManufactureDate.HasValue)
                query = query.Where(p => p.ManufactureDate >= productFilter.ProductManufactureDate.Value.Date
                                         && p.ManufactureDate < productFilter.ProductManufactureDate.Value.Date.AddDays(1));

            if (productFilter.ProductExpirationDate.HasValue)
                query = query.Where(p => p.ExpirationDate >= productFilter.ProductExpirationDate.Value.Date
                                         && p.ExpirationDate < productFilter.ProductExpirationDate.Value.Date.AddDays(1));

            if (productFilter.SupplierId.HasValue)
                query = query.Where(p => p.Supplier.Id == productFilter.SupplierId.Value);

            if (!string.IsNullOrEmpty(productFilter.SupplierDescription))
                query = query.Where(p => p.Supplier.Description.Equals(productFilter.SupplierDescription));

            if (!string.IsNullOrEmpty(productFilter.SupplierCnpj))
                query = query.Where(p => p.Supplier.Cnpj.Equals(productFilter.SupplierCnpj));

            if (!string.IsNullOrEmpty(productFilter.ProductDescription))
                query = query.Where(p => p.Description.Contains(productFilter.ProductDescription));

            var totalCount = await query.CountAsync();

            var products = await query.Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return PagedResult<Product>.Create(products, totalCount, pageIndex, pageSize);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}