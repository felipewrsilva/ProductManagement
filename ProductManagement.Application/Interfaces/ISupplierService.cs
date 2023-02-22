using ProductManagement.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAsync();
        Task<SupplierDTO> GetByIdAsync(int id);
        Task<SupplierDTO> CreateAsync(SupplierDTO supplierDto);
        Task<SupplierDTO> UpdateAsync(SupplierDTO supplierDto);
        Task<bool> DeleteAsync(int id);
    }
}